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
            //Check if the branch was created
                //if not create the branch
            //if not folder exists
                //create
                //pendadd
            throw new NotImplementedException();
        }
    }
}
