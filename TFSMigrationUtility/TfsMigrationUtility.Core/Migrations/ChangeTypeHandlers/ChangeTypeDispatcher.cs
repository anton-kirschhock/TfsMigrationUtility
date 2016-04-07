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

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers
{
    /// <summary>
    /// Handles the dispatching of a new change to a certain ChangeTypeHandler
    /// </summary>
    public static class ChangeTypeDispatcher
    {
        public static void Dispatch(MigrationConfig config,
            VersionControlServer sourceServer,
            VersionControlServer targetServer,
            Dictionary<BranchInformation, bool> branchInformation,
            TFSWorkspace targetWorkspace,
            IWorkspaceHandler workspacehander,
            Change change,
            string newComment,
            VersionSpec version){
            IProgressManager progressmanager = ServiceLocator.Get<IProgressManager>();
            try
            {
                Item changeItem = change.Item;
                if (!change.ChangeType.HasFlag(ChangeType.Delete))//TODO check if there are more of these weird changetypes where there is no item!
                {
                    changeItem = sourceServer.GetItem(change.Item.ServerItem,version);
                }
                string localpath = Utilities.TFSPathUtilities.GetLocalPath(changeItem.ServerItem, config.SourceProject, config.LocalPath);
                string newRemotePath = Utilities.TFSPathUtilities.CreateTFSPathFromSource(config.TargetProject,config.SourceProject,changeItem.ServerItem);
                if (changeItem.ItemType == ItemType.File)
                {
                    //get the file handler
                } else if (changeItem.ItemType == ItemType.Folder)
                {
                    //get the folderhandler
                }
            }
            catch (VersionControlException vce)
            {
                progressmanager.WriteException($"VersionControlException while handling {change.Item.ServerItem} with changetype {change.ChangeType}", vce);
            }
        }
    }
}
