using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Progress;
using TfsMigrationUtility.Core.Throwables;
using TfsMigrationUtility.Core.Utilities;

namespace TfsMigrationUtility.Core.Migrations.Workspace
{
    public class WorkspaceHandler : IWorkspaceHandler
    {
        public string LocalRoot { get; set; }

        public string SourceRoot { get; set; }

        public VersionControlServer SourceServer { get; set; }
        public bool IsAutoClean { get; set; }

        private IProgressManager ProgressManager { get; set; }
        public WorkspaceHandler(string localroot, string sourceroot, VersionControlServer sourceserver,bool autoclean= false)
        {
            LocalRoot = localroot;
            SourceRoot = sourceroot;
            SourceServer = sourceserver;
            ProgressManager = ServiceLocator.Get<IProgressManager>();
        }

        private string ResolveLocalPath(string sourceremotepath)
        {
            return TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(sourceremotepath, SourceRoot, LocalRoot);
        }

        public bool CreateDirectory(string sourceremotepath)
        {
            if (!DirectoryExists(sourceremotepath))
            {
                string localpath = ResolveLocalPath(sourceremotepath);
                try
                {
                    Directory.CreateDirectory(localpath);
                    return true;
                }
                catch (Exception e)
                {
                    ProgressManager.WriteException($"Exception occured while creating a directory in {localpath}", e);
                    return false;
                }
            }
            return true;
        }

        public bool DirectoryExists(string sourceremotepath)
        {
            string localpath = ResolveLocalPath(sourceremotepath);
            try
            {
                return Directory.Exists(localpath);

            }
            catch (Exception e)
            {
                ProgressManager.WriteException($"Exception occured while checking a directory in {localpath}", e);
                return false;
            }
        }

        public bool DownloadItem(string sourceremotepath)
        {
            string localpath = ResolveLocalPath(sourceremotepath);
            try
            {
                SourceServer.DownloadFile(sourceremotepath, localpath);
                return true;
            }
            catch (Exception e)
            {
                ProgressManager.WriteException($"Exception occured while downloading {sourceremotepath} to {localpath}", e);
                return false;
            }
        }

        public bool FileExists(string sourceremotepath)
        {
            string localpath = ResolveLocalPath(sourceremotepath);
            try
            {
                return File.Exists(localpath);

            }
            catch (Exception e)
            {
                ProgressManager.WriteException($"Exception occured while checking a file in {localpath}", e);
                return false;
            }
        }

        public bool RemoveItem(string sourceremotepath)
        {
            if (!DirectoryExists(sourceremotepath))
            {
                string localpath = ResolveLocalPath(sourceremotepath);
                try
                {
                    FileAttributes attributes = File.GetAttributes(localpath);
                    if (attributes.HasFlag(FileAttributes.Directory))
                    {
                        Directory.Delete(localpath);
                    }
                    else
                    {
                        File.Delete(localpath);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    ProgressManager.WriteException($"Exception occured while removing an item in {localpath}", e);
                    return false;
                }
            }
            return true;
        }

        public bool TryPrepareWorkspace()
        {
            try
            {
                PrepareWorkspace();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void PrepareWorkspace()
        {
            try
            {
                var dExists = Directory.Exists(LocalRoot);
                if (!dExists)
                {
                    Directory.CreateDirectory(LocalRoot);
                }
                else if (dExists && IsAutoClean)
                {
                    Directory.Delete(LocalRoot);
                    Directory.CreateDirectory(LocalRoot);
                }
                else if(dExists && DirectoryUtilities.CountFiles(LocalRoot)!=0)
                {
                    throw new InvalidWorkspaceSetupException("Workspace is not empty!");
                }
            }
            catch(Exception e)
            {
                ProgressManager.WriteException($"Exception occured while preparing the workspace in {this.LocalRoot}", e);
                throw new InvalidWorkspaceSetupException(e.Message, e);
            }
        }
    }
}
