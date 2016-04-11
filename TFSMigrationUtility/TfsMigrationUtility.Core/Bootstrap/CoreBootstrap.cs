using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Migrations;
using TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers;
using TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.File;
using TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.Folder;
using TfsMigrationUtility.Core.Progress;

namespace TfsMigrationUtility.Core.Bootstrap
{
    public class CoreBootstrap : IBootstrap
    {
        public void Bootstrap()
        {
            //Register the MigrationHandler
            ServiceLocator.Set<IMigrationHandler, MigrationHandler>();
            //Register the ProgressManager
            ServiceLocator.Set<IProgressManager, ProgressManager>();
            //Register the mappings
            ServiceLocator.Set<IItemTypeMapping>(new FolderTypeMapping(),ItemType.Folder.ToString());
            ServiceLocator.Set<IItemTypeMapping>(new FileTypeMapping(),ItemType.File.ToString());
            #region "Change Handlers"
            new FolderAddChangeTypeHandler().RegisterType();
            new FolderDeleteChangeTypeHandler().RegisterType();
            new FolderRenameChangeTypeHandler().RegisterType();

            new FileAddChangeTypeHandler().RegisterType();
            new FileDeleteChangeTypeHandler().RegisterType();
            new FileEditChangeTypeHandler().RegisterType();
            new FileRenameChangeTypeHandler().RegisterType();
            #endregion

        }
    }
}
