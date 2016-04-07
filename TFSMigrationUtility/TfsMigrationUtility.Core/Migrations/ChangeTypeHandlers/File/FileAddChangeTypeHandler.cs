using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.File
{
    public class FileAddChangeTypeHandler : AbstractFileChangeTypeHandler
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
            ProgressManager.WriteDebug($"Downloading {changeItem.ServerItem}...");
            changeItem.DownloadFile(localpath);
            targetWorkspace.PendAdd(localpath);
            ProgressManager.WriteDebug($"Added {changeItem.ServerItem} with new path {newremotepath}");
            return true;
        }
    }
}
