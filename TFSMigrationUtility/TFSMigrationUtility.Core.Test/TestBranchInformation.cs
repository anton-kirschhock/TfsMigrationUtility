using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Migrations;

namespace TFSMigrationUtility.Core.Test
{
    [TestClass]
    public class TestBranchInformation
    {
        BranchInformation TestCase1 = new BranchInformation()
        {
            BranchName = "$/Project/Test",
            BranchParent = "$/Project/Dev"
        };
        BranchInformation TestCase1_copy = new BranchInformation()
        {
            BranchName = "$/Project/Test",
            BranchParent = "$/Project/Dev"
        };
        BranchInformation TestCase2 = new BranchInformation()
        {
            BranchName = "$/Project/Production",
            BranchParent = "$/Project/Test"
        };

        [TestMethod]
        public void Test_BranchInformation_Equals_BranchInformation()
        {
            Assert.IsTrue(TestCase1.Equals(TestCase1_copy));
        }
        [TestMethod]
        public void Test_BranchInformation_Equals_BranchInformation_NotEquals()
        {
            Assert.IsFalse(TestCase1.Equals(TestCase2));
        }
        [TestMethod]
        public void Test_BranchInformation_Equals_BranchInformation_Null()
        {
            Assert.IsFalse(TestCase1.Equals((BranchInformation)null));
        }
        //
        [TestMethod]
        public void Test_BranchInformation_Equals_String()
        {
            Assert.IsTrue(TestCase1.Equals(TestCase1_copy.BranchName));
        }
        [TestMethod]
        public void Test_BranchInformation_Equals_String_NotEquals()
        {
            Assert.IsFalse(TestCase1.Equals(TestCase2.BranchName));
        }
        [TestMethod]
        public void Test_BranchInformation_Equals_String_Null()
        {
            Assert.IsFalse(TestCase1.Equals((string)null));
        }

        [TestMethod]
        public void Test_BranchInformationList_Contains_True()
        {
            List<BranchInformation> branches = new List<BranchInformation>()
            {
                TestCase1,
                TestCase2
            };
            for(int i=0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                });
            }
            Assert.IsTrue(branches.Contains(TestCase1_copy));
        }
        [TestMethod]
        public void Test_BranchInformationList_Contains_False()
        {
            List<BranchInformation> branches = new List<BranchInformation>()
            {
                TestCase2
            };
            for (int i = 0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                });
            }
            Assert.IsFalse(branches.Contains(TestCase1_copy));
        }

        [TestMethod]
        public void Test_BranchInformationList_Contains_String_True()
        {
            List<BranchInformation> branches = new List<BranchInformation>()
            {
                TestCase1,
                TestCase2
            };
            for (int i = 0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                });
            }
            Assert.IsTrue(branches.ContainsBranch(TestCase1_copy.BranchName));
        }
        [TestMethod]
        public void Test_BranchInformationList_Contains_String_False()
        {
            List<BranchInformation> branches = new List<BranchInformation>()
            {
                TestCase2
            };
            for (int i = 0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                });
            }
            Assert.IsFalse(branches.ContainsBranch(TestCase1_copy.BranchName));
        }
        [TestMethod]
        public void Test_BranchInformationDictionary_Contains_String_True()
        {
            Dictionary<BranchInformation,bool> branches = new Dictionary<BranchInformation,bool>()
            {
                { TestCase1,false },
                { TestCase2,false }
            };
            for (int i = 0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                },false);
            }
            Assert.IsTrue(branches.ContainsBranch(TestCase1_copy.BranchName));
        }
        [TestMethod]
        public void Test_BranchInformationDictionary_Contains_String_False()
        {
            Dictionary<BranchInformation, bool> branches = new Dictionary<BranchInformation, bool>()
            {
                { TestCase2,false }
            };
            for (int i = 0; i < 5; i++)
            {
                branches.Add(new BranchInformation()
                {
                    BranchName = "$/Project/" + i,
                    BranchParent = "$/Project/Dev"
                },false);
            }
            Assert.IsFalse(branches.ContainsBranch(TestCase1_copy.BranchName));
        }
    }
}
