using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Throwables
{
    public class ProjectNotFoundException : AppException
    {
        public ProjectNotFoundException(string message) : base(message)
        {
        }
    }
}
