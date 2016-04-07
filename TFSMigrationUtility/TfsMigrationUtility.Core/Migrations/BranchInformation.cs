using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Progress;

namespace TfsMigrationUtility.Core.Migrations
{
    public class BranchInformation
    {
        public string BranchName { get; set; }
        public string BranchParent { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is BranchInformation)
            {
                var bobj = obj as BranchInformation;
                return string.Compare(BranchName, bobj.BranchName, true) == 0 
                    && string.Compare(BranchParent, bobj.BranchParent,true) == 0;
            }else if(obj is string)//can compare to string => use branchname to check
            {
                return string.Compare(BranchName, obj as string, true) == 0;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return BranchParent.GetHashCode() * BranchName.GetHashCode() * 7;
        }
        public static List<BranchInformation> GetAllBranches(string projectroot, VersionControlServer versioncontrolserver,IProgressManager ProgressManager)
        {
            List<BranchInformation> res = new List<BranchInformation>();
            ProgressManager.WriteDebug("Resolving all branches...");
            var branches = versioncontrolserver.QueryRootBranchObjects(RecursionType.Full);
            ProgressManager.WriteDebug("Resolved all branches! Searching for project branches");
            foreach (var branch in branches)
            {
                if (branch.Properties.RootItem.Item.Contains(projectroot))
                {
                    BranchInformation branchinfo = new BranchInformation()
                    {
                        BranchName = branch.Properties.RootItem.Item,
                        BranchParent = (branch.Properties.ParentBranch != null ? null : branch.Properties.ParentBranch.Item)
                    };
                    res.Add(branchinfo);
                    ProgressManager.WriteDebug($"Found branch:{branchinfo.BranchName}");
                }
            }
            ProgressManager.WriteDebug($"Done resolving project branches");
            return res;
        }
    }
}
