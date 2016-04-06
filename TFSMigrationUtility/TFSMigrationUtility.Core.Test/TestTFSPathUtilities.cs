using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TFSMigrationUtility.Core.Test
{
    [TestClass]
    public class TestTFSPathUtilities
    {
        private readonly string remotepath = "$/TFSMigrationUtility/Development/Core/Configuration.cs";
        private readonly string remoteroot = "$/TFSMigrationUtility/";
        private readonly string localroot = "C:\\TFSMigrationUtility";
        private readonly string localpath_shouldbe = "C:\\TFSMigrationUtility\\Development\\Core\\Configuration.cs";

        private readonly string remotetargetroot = "$/Someother/Trunk/";
        private readonly string remotetarget_shouldbe = "$/Someother/Trunk/Development/Core/Configuration.cs";

        [TestMethod]
        public void Test_GetLocalPath()
        {
            Assert.AreEqual(localpath_shouldbe, TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(remotepath,remoteroot,localroot));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetLocalPath_remotepath_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(null, remoteroot, localroot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetLocalPath_remoteroot_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(remotepath, null, localroot);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetLocalPath_localroot_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(remotepath, remoteroot, null);
        }
        [TestMethod]
        public void Test_GetLocalPath_remotepath_Is_remoteroot()//localroot ==localroot
        {
            Assert.AreEqual(localroot, TfsMigrationUtility.Core.Utilities.TFSPathUtilities.GetLocalPath(remoteroot, remoteroot, localroot));
        }


        [TestMethod]
        public void Test_CreateTFSPathFromSource()
        {
            Assert.AreEqual(remotetarget_shouldbe, TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(this.remotetargetroot, remoteroot, remotepath));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateTFSPathFromSource_targetRoot_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(null, remoteroot, remotepath);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateTFSPathFromSource_sourceRoot_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(this.remotetargetroot, null, remotepath);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateTFSPathFromSource_path_null()
        {
            TfsMigrationUtility.Core.Utilities.TFSPathUtilities.CreateTFSPathFromSource(this.remotetargetroot, remoteroot, null);
        }



    }
}
