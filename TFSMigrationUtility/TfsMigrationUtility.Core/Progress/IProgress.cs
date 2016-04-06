using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Progress
{
    public interface IProgress
    {
        void OnProgress(int currentstep, int maxstep, string description);
        void WriteDebug(string message);
        void WriteException(string message, Exception ex);
    }
}
