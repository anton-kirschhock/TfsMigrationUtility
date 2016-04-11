using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Migrations.ChangeTypeHandlers;

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
        public static IEnumerable<TProxyType> GetAll<TProxyType>()
        {
            return _container.ResolveAll<TProxyType>();
        }
        public static TProxyType Get<TProxyType>(string key)
        {
            return _container.Resolve<TProxyType>(key);
        }

        public static void Set<TProxyType>(TProxyType implementation,bool IsTransient=false)
        {
            if (IsTransient) { 
                _container.RegisterInstance<TProxyType>(implementation,new TransientLifetimeManager());
            }
            else
            {
                _container.RegisterInstance<TProxyType>(implementation);
            }
        }

        public static void Set<TProxyType>(TProxyType implementation, string key, bool IsTransient = false)
        {
            if (IsTransient)
            {
                _container.RegisterInstance<TProxyType>(key, implementation,new TransientLifetimeManager());
            }
            else
            {
                _container.RegisterInstance<TProxyType>(key, implementation);
            }
        }
        public static void Set<TProxyType,TImplementation>(string key, bool IsTransient = false) where TImplementation : TProxyType
        {
            if (IsTransient)
            {
                _container.RegisterType<TProxyType,TImplementation>(key,new TransientLifetimeManager());
            }
            else
            {
                _container.RegisterType(typeof(TProxyType), typeof(TImplementation), key);
            }
        }

        public static void Set<TProxyType, TImplementation>(bool IsTransient = false) where TImplementation:TProxyType
        {
            if (IsTransient)
            {
                _container.RegisterType<TProxyType, TImplementation>("",new TransientLifetimeManager());
            }
            else
            {
                _container.RegisterType<TProxyType, TImplementation>();
            }
        }
    }
}
