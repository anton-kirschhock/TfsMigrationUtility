using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Configuration
{
    public class MigrationConfig
    {
        public bool DebugMode { get; set; } = false;
        //TFS from
        public string SourceProject { get; set; } = "$/";

        //TFS To
        public string TargetProject { get; set; } = "$/";
    }
}
