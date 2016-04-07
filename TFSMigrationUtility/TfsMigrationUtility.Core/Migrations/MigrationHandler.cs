using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Progress;
using TfsMigrationUtility.Core.Throwables;

namespace TfsMigrationUtility.Core.Migrations
{
    public class MigrationHandler : IMigrationHandler
    {
        private IProgressManager ProgressManager{ get;set; }
        public MigrationHandler()
        {
            this.ProgressManager = ServiceLocator.Get<IProgressManager>();
        }
        public Task Migrate(MigrationConfig config)
        {
            try {
                ProgressManager.InvokeProgress(0, 5, "Validating configuration...");
                config.Validate();
                ProgressManager.InvokeProgress("Configuration is valid!");

            }
            catch(InvalidConfigurationException configex) {
                ProgressManager.WriteException($"{configex.PropertyName}=>{configex.Message}", configex);
            }catch(Exception e){
                ProgressManager.WriteException($"{e.Message}", e);
            }
            throw new NotImplementedException();
        }


    }
}
