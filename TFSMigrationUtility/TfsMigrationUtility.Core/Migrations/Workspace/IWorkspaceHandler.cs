using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Migrations.Workspace
{
    public interface IWorkspaceHandler
    {
        string SourceRoot { get; set; }
        string LocalRoot { get; set; }
        
        bool DirectoryExists(string sourceremotepath);
        bool FileExists(string sourceremotepath);
        bool CreateDirectory(string sourceremotepath);
        bool DownloadItem(string sourceremotepath);
        bool RemoveItem(string sourceremotepath);
        bool PrepareWorkspace();
    }
}
