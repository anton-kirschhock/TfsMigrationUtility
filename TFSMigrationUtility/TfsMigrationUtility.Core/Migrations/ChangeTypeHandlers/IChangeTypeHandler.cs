using Microsoft.TeamFoundation.VersionControl.Client;
using TFSWorkspace = Microsoft.TeamFoundation.VersionControl.Client.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;
using TfsMigrationUtility.Core.Utilities;
using TfsMigrationUtility.Core.Progress;

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

        void RegisterType();
    }
    public abstract class AbstractChangeTypeHandler : IChangeTypeHandler
    {
        public abstract ChangeType ChangeType { get; }
        public abstract ItemType ItemType { get; }
        public IProgressManager ProgressManager
        {
            get
            {
                return ServiceLocator.Get<IProgressManager>();
            }
        }
        public abstract bool HandleChange(MigrationConfig config, VersionControlServer sourceServer, VersionControlServer targetServer, Dictionary<BranchInformation, bool> branchInformation, TFSWorkspace targetWorkspace, IWorkspaceHandler workspacehander, Change change, string newComment, VersionSpec version, string localpath, string newremotepath, Item changeItem);
        public void RegisterType()
        {
            //internal convention: EnumUtilities.GetUsableFlagString(Itemtype)/EnumUtilities.GetUsableFlagString(Changetype)
            ServiceLocator.Add<IChangeTypeHandler>(this, (EnumUtilities.GetUsableFlagString(ItemType) + "/" + EnumUtilities.GetUsableFlagString(ChangeType)));
        }
    }

    public abstract class AbstractFileChangeTypeHandler : AbstractChangeTypeHandler
    {
        public override ItemType ItemType { get { return ItemType.File; } }
    }
    public abstract class AbstractFolderChangeTypeHandler : AbstractChangeTypeHandler
    {
        public override ItemType ItemType { get { return ItemType.Folder; } }
    }
}
