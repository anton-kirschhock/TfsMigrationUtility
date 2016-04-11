using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations;
using TfsMigrationUtility.UI.ViewModels;
using TfsMigrationUtility.UI.ViewModels.Nested;

namespace TfsMigrationUtility.UI.Workers
{
    public class MigrationWorker
    {
        public bool IsRunning { get; set; } = false;

        public async Task<bool> Start(IConfigurationViewModel uiconfig,IProgressViewModel progressindicator)
        {
            //Evaluate config
            IsRunning = true;
            progressindicator.UpperMaxStep.Value = uiconfig.Projects.Value.Length;
            progressindicator.UpperCurrentStep.Value = 0;
            MigrationHandler handler = new MigrationHandler();
            string baseLocalPath = uiconfig.LocalPath.Value;
            MigrationConfig config = new MigrationConfig()
            {
                AutoCleanLocalPath = uiconfig.CleanupLocal.Value,
                DebugMode = uiconfig.DebugMode.Value,
                FullCollection = !uiconfig.MigrateSingleProject.Value,
                SourceProjectCollection = uiconfig.SourceTFS.Value,
                TargetProjectCollection = uiconfig.TargetTFS.Value,
                AutoCreateProject = uiconfig.AutocreateProject.Value
            };
            int passes = 0;
            foreach (var project in uiconfig.Projects.Value)
            {
                progressindicator.CurrentStatus.Value = $"Preparing migration for {project}";
                config.SourceProject = "$/" + project;
                config.TargetProject = config.SourceProject;
                config.LocalPath = Path.Combine(baseLocalPath, project);
                progressindicator.WriteDebug($"Migrating project {config.SourceProject} with local workspace {config.LocalPath}...");
                progressindicator.CurrentStatus.Value = $"Migrating {project}";
                //await Task.Delay(2000);//Simulate workload
                var b = await handler.Migrate(config);
                if (b)
                {
                    progressindicator.Info.AppendLine($"SUCCESS: '{project}' migrated successfully");
                    passes++;
                }
                else {
                    progressindicator.Info.AppendLine($"WARNING: '{project}' FAILED MIGRATING (see Exception log)");
                }
                progressindicator.UpperCurrentStep.Value++;
            }
            progressindicator.CurrentStatus.Value = $"Done Migrating! {passes}/{uiconfig.Projects.Value.Length} passed.";
            IsRunning = false;
            return true;
        }
    }
}
