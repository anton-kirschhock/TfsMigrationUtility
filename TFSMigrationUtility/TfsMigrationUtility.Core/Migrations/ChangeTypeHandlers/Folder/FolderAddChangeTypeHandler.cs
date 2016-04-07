using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;
using System.IO;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.Folder
{
    public class FolderAddChangeTypeHandler : AbstractFolderChangeTypeHandler
    {
        public override ChangeType ChangeType
        {
            get
            {
                return ChangeType.Add;
            }
        }

        public override bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, Microsoft.TeamFoundation.VersionControl.Client.Workspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem)
        {
            if (branchInformation.ContainsBranch(changeItem.ServerItem) && !branchInformation.GetBranch(changeItem.ServerItem).Value)
            {
                //if it is a branch and it is not created yet
                ProgressManager.WriteDebug($"{newremotepath} is a branch...");
                try
                {
                    var branchinfo = branchInformation.GetBranch(changeItem.ServerItem).Key;
                    if (branchinfo.BranchParent == null)
                    {
                        //Root branch (branch without parent branch) => Create a folder and convert to branch.
                        if (!Directory.Exists(localpath))
                        {
                            Directory.CreateDirectory(localpath);
                            targetWorkspace.PendAdd(localpath);
                            workspacehander.Checkin(config, targetWorkspace, $"{newComment} + creating branch {branchinfo.BranchName}");
                        }
                        targetServer.CreateBranchObject(new BranchProperties(new ItemIdentifier(newremotepath)));
                    }
                    else
                    {
                        if (Directory.Exists(localpath))
                        {
                            Directory.Delete(localpath);
                            targetWorkspace.PendDelete(localpath, RecursionType.Full);
                        }
                        string targetParentBranch = TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(config.TargetProject, config.SourceProject, branchinfo.BranchParent);
                        workspacehander.Checkin(config, targetWorkspace, $"{newComment} + branching {targetParentBranch} {branchinfo.BranchName}");
                        targetServer.CreateBranch(targetParentBranch, newremotepath, VersionSpec.Latest, null, $"{newComment} + branching {targetParentBranch} {branchinfo.BranchName}", null, null, null);
                    }
                    branchInformation[branchinfo] = true;
                }
                catch (Exception ex)
                {
                    ProgressManager.WriteException($"Error occured while branching {newremotepath}:{ex.Message}", ex);
                    return false;
                }
            }
            else
            {
                //Create the directory if it doesnt exists
                if (!Directory.Exists(localpath))
                {
                    Directory.CreateDirectory(localpath);
                    targetWorkspace.PendAdd(localpath);
                    ProgressManager.WriteDebug($"added {newremotepath}");
                    return true;
                }
            }
            return false;
               
        }
    }
}
