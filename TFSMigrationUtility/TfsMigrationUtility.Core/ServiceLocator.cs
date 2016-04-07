using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.Core
{
    public static class ServiceLocator
    {
        private static UnityContainer _container;

        static ServiceLocator()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);
            Bootstrap.Bootstrapper.Bootstrap();//bootstrap the application
        }
        public static TProxyType Get<TProxyType>()
        {
            return _container.Resolve<TProxyType>();
        }
        public static TProxyType Get<TProxyType>(string key)
        {
            return _container.Resolve<TProxyType>(key);
        }

        public static void Add<TProxyType>(TProxyType implementation)
        {
            _container.RegisterInstance<TProxyType>(implementation);
        }
        public static void Add<TProxyType>(TProxyType implementation, string key)
        {
            _container.RegisterInstance<TProxyType>(key, implementation);
        }

        public static void Add<TProxyType, TImplementation>() where TImplementation:TProxyType
        {
            _container.RegisterType<TProxyType,TImplementation>();
        }
    }
}
