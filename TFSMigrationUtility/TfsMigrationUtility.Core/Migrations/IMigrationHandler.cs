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
        Task Migrate(MigrationConfig config);
    }
}
