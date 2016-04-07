using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Configuration
{
    public class MigrationConfig
    {
        /// <summary>
        /// Indicates if the tool has to print out everything
        /// </summary>
        public bool DebugMode { get; set; } = false;

        /// <summary>
        /// Indicates if the tool has to migrate the full Collection
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

        public override string ToString()
        {
            return $"MigrateConfiguration:{(this.FullCollection?"the full collection":$"{SourceProject} to {TargetProject}")} from {SourceProjectCollection.Uri.ToString()} to {TargetProjectCollection.Uri.ToString()}{(DebugMode?" in debugmode":"")}";
        }
    }
}
