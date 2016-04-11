using Microsoft.TeamFoundation.VersionControl.Client;
using TFSWorkspace = Microsoft.TeamFoundation.VersionControl.Client.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;
using TfsMigrationUtility.Core.Progress;
using TfsMigrationUtility.Core.Throwables;
using TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers;

namespace TfsMigrationUtility.Core.Migrations
{
    public class MigrationHandler : IMigrationHandler
    {
        private IProgressManager ProgressManager { get; set; }

        public bool IsRunning { get; private set; } = false;

        public MigrationHandler()
        {
            this.ProgressManager = ServiceLocator.Get<IProgressManager>();
        }
        public async Task<bool> Migrate(MigrationConfig config)
        {
            bool res = true;
            try
            {
                IsRunning = true;
                ProgressManager.InvokeProgress(0, 5, "Validating configuration...");
                config.Validate();
                ProgressManager.InvokeProgress("Configuration is valid!");
                ProgressManager.InvokeProgress("Connecting to version control servers...", false);
                config.SourceProjectCollection.EnsureAuthenticated();
                config.TargetProjectCollection.EnsureAuthenticated();
                VersionControlServer sourceServer = config.SourceProjectCollection.GetService<VersionControlServer>();
                VersionControlServer targetServer = config.TargetProjectCollection.GetService<VersionControlServer>();
                ProgressManager.InvokeProgress("Connected!");
                ProgressManager.InvokeProgress("Checking projects...", false);
                if (!sourceServer.ServerItemExists(config.SourceProject, ItemType.Any))
                {
                    throw new ProjectNotFoundException($"Project {config.SourceProject} does not exist on the source server!");
                }
                if (!sourceServer.ServerItemExists(config.TargetProject, ItemType.Any))
                {
                    if (config.AutoCreateProject)
                    {
                        ProgressManager.InvokeProgress("Autocreation is not implemented yet!", false);
                    }
                    //else
                    throw new ProjectNotFoundException($"Project {config.TargetProject} does not exist on the target server! Please create the project or enable the AutoCreate option and try again");
                }
                ProgressManager.InvokeProgress("Preparing local workspace...", false);
                IWorkspaceHandler workspacehandler = new WorkspaceHandler(config.LocalPath, config.SourceProject, sourceServer, config.AutoCleanLocalPath);
                workspacehandler.PrepareWorkspace();
                TFSWorkspace targetWorkspace = null;
                try
                {
                    targetWorkspace = targetServer.GetWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                    targetServer.DeleteWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                }
                catch { }
                targetWorkspace = targetServer.CreateWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                targetWorkspace.Map(config.TargetProject, config.LocalPath);
                ProgressManager.InvokeProgress("Workspace is ready!");
                ProgressManager.InvokeProgress("Gathering branches...", false);
                //Key = Branch, value = IsCreated
                Dictionary<BranchInformation, bool> branchInformation
                    = BranchInformation.GetAllBranches(config.SourceProject, sourceServer, ProgressManager)
                        .ToDictionary(a => a, a => false);
                ProgressManager.InvokeProgress($"Found {branchInformation.Count()} branches in total!");
                await MigrateWorker(config, sourceServer, targetServer, branchInformation, targetWorkspace, workspacehandler);

            }
            catch (ProjectNotFoundException pnfe)
            {
                ProgressManager.WriteException($"{pnfe.Message}", pnfe);
                res = false;
            }
            catch (InvalidConfigurationException configex)
            {
                ProgressManager.WriteException($"{configex.PropertyName}=>{configex.Message}", configex);
                res = false;
            }
            catch (ChangeTypeHandlerException cthe)
            {
                ProgressManager.WriteException($"{cthe.ItemType},{cthe.ChangeType}=>{cthe.Message}", cthe);
                res = false;
            }
            catch (Exception e)
            {
                ProgressManager.WriteException($"{e.Message}", e);
                res = false;
            }
            //Cleanup:
            if (config.AutoCleanLocalPath)
            {
                try
                {
                    //cleanup
                    ProgressManager.InvokeProgress(0, 1, "Starting local directory cleanup...");
                    if (System.IO.Directory.Exists(config.LocalPath))
                    {
                        System.IO.Directory.Delete(config.LocalPath,true);
                    }
                    ProgressManager.InvokeProgress("Done with cleanup");
                }
                catch (Exception e)
                {
                    ProgressManager.WriteException($"Something happend while cleaning the local path: {e.Message}", e);
                }
            }
            IsRunning = false;
            return res;
        }

        private async Task MigrateWorker(MigrationConfig config,
            VersionControlServer sourceServer,
            VersionControlServer targetServer,
            Dictionary<BranchInformation, bool> branchInformation,
            TFSWorkspace targetWorkspace,
            IWorkspaceHandler workspacehander)
        {
            try
            {
                ProgressManager.InvokeProgress("Gathering history...", false);
                var changesets = sourceServer.QueryHistory(config.SourceProject, RecursionType.Full, Int32.MaxValue);
                int changesetcount = changesets.Count();
                ProgressManager.InvokeProgress(0, changesetcount, $"{changesetcount} changesets available");
                foreach (Changeset changeset in changesets.OrderBy(a => a.CreationDate))
                {
                    ProgressManager.InvokeProgress($"Migrating changeset with ID {changeset.ChangesetId}", false);
                    string newcomment = changeset.Comment;
                    if (!newcomment.ToLower().Contains("original checkin:"))
                    {
                        #region "New Comment with meta data creation"
                        newcomment =
$@"{(string.IsNullOrWhiteSpace(changeset.Comment) ? "No comment provided" : changeset.Comment)}
--
Original checkin:
    * Who: {changeset.Committer} ({changeset.CommitterDisplayName})
    * When: {changeset.CreationDate}
    * In: {changeset.VersionControlServer.TeamProjectCollection.Uri}
    * ID: {changeset.ChangesetId}";
                        #endregion
                    }
                    int originalstep = ProgressManager.CurrentStep;
                    var changes = sourceServer.GetChangesForChangeset(changeset.ChangesetId, true, int.MaxValue, null, null, true);
                    ProgressManager.InvokeProgress(0, changes.Length, $"Found {changes.Length} changes in changeset {changeset.ChangesetId}");
                    var version = new ChangesetVersionSpec(changeset.ChangesetId);
                    foreach (Change change in changes)
                    {
                        ChangeTypeDispatcher.Dispatch(config, sourceServer, targetServer, branchInformation, targetWorkspace, workspacehander, change, newcomment, version);
                    }
                    workspacehander.Checkin(config, targetWorkspace, newcomment);
                    ProgressManager.InvokeProgress(originalstep, changesetcount, $"Finished with itterating over {changeset.ChangesetId}");
                    ProgressManager.InvokeProgress($"Migration of changeset with ID {changeset.ChangesetId} completed");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
