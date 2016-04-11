using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;

namespace TfsMigrationUtility.Core.Migrations
{
    public interface IMigrationHandler
    {
        bool IsRunning { get; }
        Task<bool> Migrate(MigrationConfig config);
    }
}
