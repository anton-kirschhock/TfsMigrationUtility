using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Utilities;

namespace TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers.Folder
{
    public class FolderTypeMapping:IItemTypeMapping
    {
        public string GetMappedHandlerKey(ChangeType type)
        {
            if (type.HasFlag(ChangeType.Add) || type.HasFlag(ChangeType.Branch))
                return (EnumUtilities.GetUsableFlagString(ItemType.Folder) + "/" + EnumUtilities.GetUsableFlagString(ChangeType.Add));
            else if (type.HasFlag(ChangeType.Edit))
                return (EnumUtilities.GetUsableFlagString(ItemType.Folder) + "/" + EnumUtilities.GetUsableFlagString(ChangeType.Edit));
            else if (type.HasFlag(ChangeType.Delete))
                return (EnumUtilities.GetUsableFlagString(ItemType.Folder) + "/" + EnumUtilities.GetUsableFlagString(ChangeType.Delete));
            else if (type.HasFlag(ChangeType.Rename))
                return (EnumUtilities.GetUsableFlagString(ItemType.Folder) + "/" + EnumUtilities.GetUsableFlagString(ChangeType.Delete));
            else return null;
        }

        public bool IsItemType(ItemType itemtype)
        {
            return itemtype.HasFlag(ItemType.Folder);
        }
    }
}
