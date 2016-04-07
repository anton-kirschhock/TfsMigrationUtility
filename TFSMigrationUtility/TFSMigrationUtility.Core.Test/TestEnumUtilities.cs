using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Utilities;

namespace TFSMigrationUtility.Core.Test
{
    [Flags]
    public enum TestEnum
    {
        Rename = 64,
        Add = 1,
        Delete = 2,
        Edit = 4,
        Branch = 8,
        Rollback = 16,
        Merge = 32
    }

    [TestClass]
    public class TestEnumUtilities
    {
        [TestMethod]
        public void Test_GetFlags_OneFlag()
        {
            AssertFlags(new TestEnum[] { TestEnum.Add });
        }
        [TestMethod]
        public void Test_GetFlags_TwoFlags()
        {
            TestEnum[] flags = new TestEnum[] { TestEnum.Add, TestEnum.Branch };
            AssertFlags(flags);
        }
        [TestMethod]
        public void Test_GetFlags_FourFlags()
        {
            TestEnum[] flags = new TestEnum[] { TestEnum.Add, TestEnum.Branch, TestEnum.Edit, TestEnum.Rollback };
            AssertFlags(flags);
        }

        [TestMethod]
        public void Test_GetFlags_AllFlags()
        {
            TestEnum[] flags = new TestEnum[] { TestEnum.Add, TestEnum.Branch, TestEnum.Delete, TestEnum.Edit,TestEnum.Merge,TestEnum.Rename,TestEnum.Rollback };
            AssertFlags(flags);
        }

        private void AssertFlags(TestEnum[] flags)
        {
            TestEnum flag = flags[0];
            for (int i = 1; i < flags.Length; i++)
            {
                flag = flag | flags[i];
            }

            List<Enum> resultFlags =EnumUtilities.GetFlags(flag).ToList();
            Assert.IsTrue(resultFlags.Count() == flags.Length);
            foreach (Enum resultFlag in resultFlags)
            {
                Assert.IsTrue(flag.HasFlag(resultFlag));
            }
        }
    }
}
