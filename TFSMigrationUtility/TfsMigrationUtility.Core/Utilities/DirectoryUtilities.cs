using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Utilities
{
    public static class DirectoryUtilities
    {

        //TODO: check if needed
        public static int CountFiles(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            return di.GetFiles("*", SearchOption.AllDirectories).Count();
        }
        //TODO: check if needed
        public static void CloneDirectory(string sourceDirectory, string targetDirectory, Action<string> incrementcallback = null)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CloneDirectory(diSource, diTarget, incrementcallback);
        }
        //TODO: check if needed
        public static void CloneDirectory(DirectoryInfo source, DirectoryInfo target, Action<string> incrementcallback = null)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                incrementcallback?.Invoke(fi.FullName);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                if (diSourceSubDir.FullName.Contains("$tf")) continue;
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CloneDirectory(diSourceSubDir, nextTargetSubDir, incrementcallback);
            }
        }
    }
}
