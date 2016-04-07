using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.Folder
{
    public class FolderRenameChangeTypeHandler : AbstractFolderChangeTypeHandler
    {
        public override ChangeType ChangeType
        {
            get
            {
                return ChangeType.Rename;
            }
        }

        public override bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, Microsoft.TeamFoundation.VersionControl.Client.Workspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem)
        {
            if(change.MergeSources != null && change.MergeSources.Count() != 0)
            {
                string oldSourceName = change.MergeSources[0].ServerItem;
                string oldname = TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(config.SourceProject, config.TargetProject, oldSourceName);
                targetWorkspace.PendRename(oldname, newremotepath);
                ProgressManager.WriteDebug($"Renamed {oldname} to {newremotepath}");
                return true;    
            }
            return false;
        }
    }
}
