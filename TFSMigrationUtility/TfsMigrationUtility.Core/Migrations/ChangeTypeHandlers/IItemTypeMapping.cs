using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers
{
    public interface IItemTypeMapping
    {
        //TODO find a better way to store the mappings

        bool IsItemType(ItemType itemtype);
        string GetMappedHandlerKey(ChangeType type);
    }
}
