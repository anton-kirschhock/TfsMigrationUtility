using Microsoft.TeamFoundation.VersionControl.Client;
using TFSWorkspace = Microsoft.TeamFoundation.VersionControl.Client.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers
{
    public interface IChangeTypeHandler
    {
        ChangeType ChangeType { get; }
        ItemType ItemType { get; }
        bool HandleChange(MigrationConfig config,
            VersionControlServer sourceServer,
            VersionControlServer targetServer,
            Dictionary<BranchInformation, bool> branchInformation,
            TFSWorkspace targetWorkspace,
            IWorkspaceHandler workspacehander,
            Change change,
            string newComment,
            VersionSpec version,
            string localpath,
            string newremotepath,
            Item changeItem);
    }

    public abstract class AbstractFileChangeTypeHandler : IChangeTypeHandler
    {
        public abstract ChangeType ChangeType { get; }
        public ItemType ItemType { get { return ItemType.File; } }

        public abstract bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, TFSWorkspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem);
    }
    public abstract class AbstractFolderChangeTypeHandler : IChangeTypeHandler
    {
        public abstract ChangeType ChangeType { get; }
        public ItemType ItemType { get { return ItemType.Folder; } }
        public abstract bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, TFSWorkspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem);
    }
}
