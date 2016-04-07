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
            ServiceLocator.Add<IMigrationHandler, MigrationHandler>();
            //Register the ProgressManager
            ServiceLocator.Add<IProgressManager, ProgressManager>();
            //Register the mappings
            ServiceLocator.Add<IItemTypeMapping>(new FolderTypeMapping(),ItemType.Folder.ToString());
            ServiceLocator.Add<IItemTypeMapping>(new FileTypeMapping(),ItemType.File.ToString());
            #region "Change Handlers"

            #endregion

        }
    }
}
