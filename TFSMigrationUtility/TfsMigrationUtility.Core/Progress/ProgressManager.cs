using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Configuration;

namespace TfsMigrationUtility.Core.Progress
{
    public interface IProgressManager
    {
        IEnumerable<IProgress> ProgressHandlers { get; }
        void InvokeProgress(int currentstep, int maxstep, string description);
        void WriteDebug(string message);
        void WriteException(string message, Exception e);
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
        public void WriteDebug(string message)
        {
            if (ServiceLocator.Get<MigrationConfig>().DebugMode)
            {
                foreach (IProgress progress in ProgressHandlers)
                {
                    progress.WriteDebug(message);
                }
            }
        }
        public void WriteException(string message, Exception e)
        {
            foreach (IProgress progress in ProgressHandlers)
            {
                progress.WriteException(message,e);
            }
        }
    }
}
