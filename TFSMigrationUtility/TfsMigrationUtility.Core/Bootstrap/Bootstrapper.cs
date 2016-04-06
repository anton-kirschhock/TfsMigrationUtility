using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core.Bootstrap
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            //resolve all IBootstrap instances in all
            var bootstrapInstances = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t =>
                    t.GetInterfaces().Contains(typeof(IBootstrap)) &&
                    t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => Activator.CreateInstance(t) as IBootstrap);

            foreach (var bootstrapInstance in bootstrapInstances)
            {
                bootstrapInstance.Bootstrap();
            }
                    
        }
    }
}
