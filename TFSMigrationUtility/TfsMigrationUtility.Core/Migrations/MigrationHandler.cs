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
        private IProgressManager ProgressManager{ get;set; }
        public MigrationHandler()
        {
            this.ProgressManager = ServiceLocator.Get<IProgressManager>();
        }
        public async Task Migrate(MigrationConfig config)
        {
            try {
                ProgressManager.InvokeProgress(0, 4, "Validating configuration...");
                config.Validate();
                ProgressManager.InvokeProgress("Configuration is valid!");
                ProgressManager.InvokeProgress("Connecting to version control servers...",false);
                VersionControlServer sourceServer = config.SourceProjectCollection.GetService<VersionControlServer>();
                VersionControlServer targetServer = config.TargetProjectCollection.GetService<VersionControlServer>();
                ProgressManager.InvokeProgress("Connected!");
                ProgressManager.InvokeProgress("Preparing local workspace...", false);
                IWorkspaceHandler workspacehandler = new WorkspaceHandler(config.LocalPath,config.SourceProject,sourceServer,config.AutoCleanLocalPath);
                workspacehandler.PrepareWorkspace();
                TFSWorkspace targetWorkspace = null;
                try
                {
                    targetWorkspace = targetServer.GetWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                    targetServer.DeleteWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                }
                catch{ }
                targetWorkspace = targetServer.CreateWorkspace("TFSMigrationUtility", targetServer.AuthorizedIdentity.UniqueName);
                targetWorkspace.Map(config.TargetProject, config.LocalPath);
                ProgressManager.InvokeProgress("Workspace is ready!");
                ProgressManager.InvokeProgress("Gathering branches...", false);
                //Key = Branch, value = IsCreated
                Dictionary<BranchInformation, bool> branchInformation 
                    = BranchInformation.GetAllBranches(config.SourceProject, sourceServer, ProgressManager)
                        .ToDictionary(a => a, a => false);
                ProgressManager.InvokeProgress($"Found {branchInformation.Count()} branches in total!");
                await MigrateWorker(config, sourceServer,targetServer, branchInformation, targetWorkspace, workspacehandler);
            }
            catch(InvalidConfigurationException configex) {
                ProgressManager.WriteException($"{configex.PropertyName}=>{configex.Message}", configex);
            }catch(Exception e){
                ProgressManager.WriteException($"{e.Message}", e);
            }
        }

        public async Task MigrateWorker(MigrationConfig config,
            VersionControlServer sourceServer,
            VersionControlServer targetServer,
            Dictionary<BranchInformation, bool> branchInformation,
            TFSWorkspace targetWorkspace,
            IWorkspaceHandler workspacehander){
            try { 
                ProgressManager.InvokeProgress("Gathering history...", false);
                var changesets = sourceServer.QueryHistory(config.SourceProject, RecursionType.Full, Int32.MaxValue);
                int changesetcount = changesets.Count();
                ProgressManager.InvokeProgress(0, changesetcount, $"{changesetcount} changesets available");
                foreach (Changeset changeset in changesets) 
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
                        int originalstep = ProgressManager.CurrentStep;
                        var changes = sourceServer.GetChangesForChangeset(changeset.ChangesetId, true, int.MaxValue, null, null, true);
                        ProgressManager.InvokeProgress(0, changes.Length, $"Found {changes.Length} changes in changeset {changeset.ChangesetId}");
                        var version = new ChangesetVersionSpec(changeset.ChangesetId);
                        foreach (Change change in changes)
                        {
                            ChangeTypeDispatcher.Dispatch(config, sourceServer,targetServer, branchInformation, targetWorkspace, workspacehander,change,newcomment,version);
                        }
                        ProgressManager.InvokeProgress(originalstep, changesetcount, $"Finished with itterating over {changeset.ChangesetId}");
                    }
                    ProgressManager.InvokeProgress($"Migration of changeset with ID {changeset.ChangesetId} completed");
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

    }
}
