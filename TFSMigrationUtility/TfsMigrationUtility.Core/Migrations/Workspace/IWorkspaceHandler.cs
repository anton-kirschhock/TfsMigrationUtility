using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;
using TFSWorkspace = Microsoft.TeamFoundation.VersionControl.Client.Workspace;
namespace TfsMigrationUtility.Core.Migrations.Workspace
{
    public interface IWorkspaceHandler
    {
        string SourceRoot { get; set; }
        string LocalRoot { get; set; }
        bool IsAutoClean { get; set; }
        VersionControlServer SourceServer { get; set; }

        bool DirectoryExists(string sourceremotepath);
        bool FileExists(string sourceremotepath);
        bool CreateDirectory(string sourceremotepath);
        bool DownloadItem(string sourceremotepath);
        bool RemoveItem(string sourceremotepath);
        bool TryPrepareWorkspace();
        void PrepareWorkspace();
        bool Checkin(MigrationConfig config, TFSWorkspace workspace, string message);
    }
}
