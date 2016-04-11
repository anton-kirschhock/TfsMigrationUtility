using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.ViewModels.Nested
{
    public interface IConfigurationViewModel
    {
        ICell<bool> MigrateSingleProject { get; set; }
        ICell<bool> DebugMode { get; set; }
        ICell<bool> CleanupLocal { get; set; }
        ICell<bool> AutocreateProject { get; set; }
        ICell<string[]> Projects { get; set; }
        ICell<TfsTeamProjectCollection> SourceTFS { get; set; }
        ICell<TfsTeamProjectCollection> TargetTFS { get; set; }
        StringCell LocalPath { get; set; }
    }
}
