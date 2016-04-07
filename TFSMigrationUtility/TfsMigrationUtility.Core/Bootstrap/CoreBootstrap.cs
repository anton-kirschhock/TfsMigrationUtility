using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Migrations;
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

        }
    }
}
