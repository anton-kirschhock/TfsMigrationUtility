using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;
using IODirectory = System.IO.Directory;
namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.Folder
{
    public class FolderDeleteChangeTypeHandler : AbstractFolderChangeTypeHandler
    {
        public override ChangeType ChangeType
        {
            get
            {
                return ChangeType.Delete;
            }
        }

        public override bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, Microsoft.TeamFoundation.VersionControl.Client.Workspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem)
        {
            if (IODirectory.Exists(localpath))
            {
                IODirectory.Delete(localpath, true);
                targetWorkspace.PendDelete(localpath, RecursionType.Full);
                ProgressManager.WriteDebug($"Removed {newremotepath} and all children");
                return true;
            }
            return false;
        }
    }
}
