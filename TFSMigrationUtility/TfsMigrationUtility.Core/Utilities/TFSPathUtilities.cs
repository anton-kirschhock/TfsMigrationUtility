using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Utilities
{
    public static class TFSPathUtilities
    {
        /// <summary>
        /// Extracts the local file path
        /// </summary>
        /// <param name="remotepath">the full remote path to the item</param>
        /// <param name="remoteroot">the project root folder</param>
        /// <param name="localroot">the local root file path </param>
        /// <returns>the extracted local file path</returns>
        public static string GetLocalPath(string remotepath, string remoteroot, string localroot)
        {
            if (remotepath == null) throw new ArgumentNullException("Parameter remotepath cannot be null!");
            if (remoteroot == null) throw new ArgumentNullException("Parameter remoteroot cannot be null!");
            if (localroot == null) throw new ArgumentNullException("Parameter localroot cannot be null!");
            string remotepart = remotepath.Replace(remoteroot, "").Replace('/', '\\');
            if (remotepart.IndexOf('\\') == 0)
            {
                remotepart = remotepart.Substring(1);
            }
            string newPath = Path.Combine(localroot, remotepart);
            Console.WriteLine(newPath);
            return newPath;
        }

        /// <summary>
        /// Creates a tfs path from a source
        /// </summary>
        /// <param name="targetRoot">the new root tfs path</param>
        /// <param name="sourceRoot">the root source tfs path</param>
        /// <param name="path">the full remote path to the item (on source)</param>
        /// <returns></returns>
        public static string CreateTFSPathFromSource(string targetRoot, string sourceRoot, string path)
        {
            if (targetRoot == null) throw new ArgumentNullException("Parameter targetRoot cannot be null!");
            if (sourceRoot == null) throw new ArgumentNullException("Parameter sourceRoot cannot be null!");
            if (path == null) throw new ArgumentNullException("Parameter path cannot be null!");

            string newpath = path.Replace(sourceRoot, "");
            if (newpath.IndexOf("/") == 0)
            {
                newpath = newpath.Substring(1);
            }
            if (targetRoot.LastIndexOf("/") == targetRoot.Length - 1)
            {
                newpath = targetRoot + newpath;
            }
            else
            {
                newpath = targetRoot + "/" + newpath;
            }
            return newpath;
        }
    }
}
