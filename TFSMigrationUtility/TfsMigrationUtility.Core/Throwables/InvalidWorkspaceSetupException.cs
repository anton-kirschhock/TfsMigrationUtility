using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Throwables
{
    public class InvalidWorkspaceSetupException:AppException
    {
        public InvalidWorkspaceSetupException(string msg) : base($"The workspace is invalid due to: {msg}") { }
        {

        }
        public InvalidWorkspaceSetupException(string msg,Exception inner):base($"The workspace is invalid due to: {msg}",inner){}
    }
}
