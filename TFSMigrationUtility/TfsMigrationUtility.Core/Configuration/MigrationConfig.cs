using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Throwables;

namespace TfsMigrationUtility.Core.Configuration
{
    public class MigrationConfig
    {
        /// <summary>
        /// Indicates if the tool has to print out everything
        /// </summary>
        public bool DebugMode { get; set; } = false;
        /// <summary>
        /// Indicates if the tool has to create the project
        /// </summary>
        public bool AutoCreateProject { get; set; } = false;
        /// <summary>
        /// Indicates if the tool has to migrate the full Collection. 
        /// Note: The core is not using this property and expects a single project each time. 
        /// The migration of multiple projects is handled by the caller
        /// </summary>
        public bool FullCollection { get; set; } = false;
        /// <summary>
        /// A valid TFSTeamProjectCollection connection to the source
        /// </summary>
        public virtual TfsTeamProjectCollection SourceProjectCollection { get; set; }
        /// <summary>
        /// The Path to the source project
        /// </summary>
        public virtual string SourceProject { get; set; } = "$/";
        /// <summary>
        /// A valid TFSTeamProjectCollection connection to the target
        /// </summary>
        public virtual TfsTeamProjectCollection TargetProjectCollection { get; set; }
        /// <summary>
        /// The path to the target project
        /// </summary>
        public virtual string TargetProject { get; set; } = "$/";
        /// <summary>
        /// The path where the migration will be stored
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Indicates if the local path should be cleaned
        /// </summary>
        public bool AutoCleanLocalPath { get; set; } = false;
        public override string ToString()
        {
            return $"MigrateConfiguration:{(this.FullCollection?"the full collection":$"{SourceProject} to {TargetProject}")} from {SourceProjectCollection.Uri.ToString()} to {TargetProjectCollection.Uri.ToString()}{(DebugMode?" in debugmode":"")}";
        }
        /// <summary>
        /// Validates the configuration without throwing an exception
        /// </summary>
        /// <returns>True if Validated, False if not</returns>
        public virtual bool TryValidate()
        {
            try
            {
                Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Validates the configuration. If a field is not valid, it will throw an exception with the name of the property
        /// </summary>
        public virtual void Validate()
        {
            //TODO make Resources for strings
            if (SourceProjectCollection == null) throw new InvalidConfigurationException(nameof(SourceProjectCollection), "A Connection to a source TFS is required!");
            if (string.IsNullOrWhiteSpace(SourceProject)) throw new InvalidConfigurationException(nameof(SourceProject), "The Source project path is required!");
            if (TargetProjectCollection == null) throw new InvalidConfigurationException(nameof(TargetProjectCollection), "A Connection to a target TFS is required!");
            if (string.IsNullOrWhiteSpace(TargetProject)) throw new InvalidConfigurationException(nameof(TargetProject), "The target project path is required!");
            if (string.IsNullOrWhiteSpace(LocalPath)) throw new InvalidConfigurationException(nameof(LocalPath), "The local workspace path is required!");
        }
    }
}
