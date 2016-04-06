using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Progress
{
    public interface IProgressManager
    {
        IEnumerable<IProgress> ProgressHandlers { get; }
        void InvokeProgress(int currentstep, int maxstep, string description);
    }
    public class ProgressManager : IProgressManager
    {
        public IEnumerable<IProgress> ProgressHandlers
        {
            get
            {

                return ServiceLocator.Get<IEnumerable<IProgress>>();
            }
        }

        public void InvokeProgress(int currentstep, int maxstep, string description)
        {
            foreach (IProgress progress in ProgressHandlers)
            {
                progress.OnProgress(currentstep,maxstep,description);
            }
        }
    }
}
