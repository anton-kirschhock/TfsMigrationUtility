using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Throwables
{
    public class ChangeTypeHandlerException : AppException
    {
        public ItemType ItemType { get; set; }
        public ChangeType ChangeType { get; set; }
        public ChangeTypeHandlerException(ItemType itemtype,ChangeType changetype, string message) : base(message)
        {
            this.ItemType = itemtype;
            this.ChangeType = changetype;
        }

        public ChangeTypeHandlerException(ItemType itemtype, ChangeType changetype, string message, Exception innerException) : base(message, innerException)
        {
            this.ItemType = itemtype;
            this.ChangeType = changetype;
        }
    }
}
