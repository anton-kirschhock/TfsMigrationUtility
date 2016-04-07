using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Utilities
{
    public static class EnumUtilities
    {
        public static IEnumerable<Enum> GetFlags(Enum e){
            // http://stackoverflow.com/questions/4171140/iterate-over-values-in-flags-enum
            foreach (Enum x in Enum.GetValues(e.GetType()))
            {
                if (e.HasFlag(x)) yield return x;
            }
        }

        public static string GetUsableFlagString(Enum e)
        {
            return e.ToString().Replace(", ", "");
        }
    }
}
