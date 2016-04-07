using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Throwables
{
    public class InvalidConfigurationException : AppException
    {
        public string PropertyName { get; set; }
        public InvalidConfigurationException(string propertyname)
        {
            this.PropertyName = PropertyName;
        }

        public InvalidConfigurationException(string propertyname,string message) : base(message)
        {
            this.PropertyName = PropertyName;
        }

        public InvalidConfigurationException(string propertyname,string message, Exception innerException) : base(message, innerException)
        {
            this.PropertyName = PropertyName;
        }
    }
}
