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
                    progressmanager.WriteDebug($"Resolving the full changeItem...");
                    changeItem = sourceServer.GetItem(change.Item.ServerItem,version);
                }
                string localpath = Utilities.TFSPathUtilities.GetLocalPath(changeItem.ServerItem, config.SourceProject, config.LocalPath);
                string newremotepath = Utilities.TFSPathUtilities.CreateTFSPathFromSource(config.TargetProject,config.SourceProject,changeItem.ServerItem);
                progressmanager.WriteDebug($"Resolving a handler for {changeItem.ItemType}/{change.ChangeType}...");
                IChangeTypeHandler handler = GetChangeTypeHandler(changeItem.ItemType, change.ChangeType);
                if(handler != null)
                {
                    progressmanager.WriteDebug($"A handler for {changeItem.ItemType}/{change.ChangeType} was located.");
                    handler.HandleChange(config,
                                         sourceServer,
                                         targetServer,
                                         branchInformation,
                                         targetWorkspace,
                                         workspacehander,
                                         change,
                                         newComment,
                                         version,
                                         localpath,
                                         newremotepath,
                                         changeItem
                    );
                }
                else
                {
                    progressmanager.WriteDebug($"A handler for {changeItem.ItemType}/{change.ChangeType} was not located. Skipping this change");
                }
            }
            catch (VersionControlException vce)
            {
                progressmanager.WriteException($"VersionControlException while handling {change.Item.ServerItem} with changetype {change.ChangeType}", vce);
            }
        }
        
        /// <summary>
        /// Resolves a Changetype, for a certain itemtype-changetype combination, from the ServiceLocator
        /// </summary>
        /// <param name="itemtype">The itemtype</param>
        /// <param name="changetype">The changetype</param>
        /// <returns>A Instance, if provided, or Null</returns>
        private static IChangeTypeHandler GetChangeTypeHandler(ItemType itemtype, ChangeType changetype)
        {
            try {
                IItemTypeMapping mapper = ServiceLocator.Get<IItemTypeMapping>(itemtype.ToString());
                return ServiceLocator.Get<IChangeTypeHandler>(mapper.GetMappedHandlerKey(changetype));
            }catch
            {
                return null;
            }
        }
    }
}
