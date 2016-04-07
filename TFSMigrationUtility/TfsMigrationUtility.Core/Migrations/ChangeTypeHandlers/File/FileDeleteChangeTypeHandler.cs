using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;
using TfsMigrationUtility.Core.Utilities;

using IOFile = System.IO.File;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.File
{
    public class FileDeleteChangeTypeHandler : AbstractFileChangeTypeHandler
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
            if (IOFile.Exists(localpath))
            {
                IOFile.Delete(localpath);
                targetWorkspace.PendDelete(localpath);
                ProgressManager.WriteDebug($"{newremotepath} is removed");
                return true;
            }
            ProgressManager.WriteDebug($"{newremotepath} was not found on local workspace");
            return false;
        }
    }
}
