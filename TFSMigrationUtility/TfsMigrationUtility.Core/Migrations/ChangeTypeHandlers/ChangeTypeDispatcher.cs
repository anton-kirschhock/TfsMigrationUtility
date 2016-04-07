using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TfsMigrationUtility.Core.Migrations.Workspace;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers
{
    /// <summary>
    /// Handles the dispatching of a new change to a certain ChangeTypeHandler
    /// </summary>
    public static class ChangeTypeDispatcher
    {
        /// <summary>
        /// Dispatches to a certain ChangeTypeHandler
        /// </summary>
        public static void Dispatch(MigrationConfig config,IWorkspaceHandler WorkspaceHandler/*TODO Parameters*/)
        {
            //TODO body
        }
    }
}
