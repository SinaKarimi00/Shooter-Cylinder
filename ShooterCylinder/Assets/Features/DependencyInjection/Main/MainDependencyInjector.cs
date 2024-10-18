using System.Collections.Generic;
using Features.ConfigProvider;
using Features.ConfigProvider.Main;
using Features.DependencyInjection.Application;
using Features.Observer.Main;
using UnityEngine;

namespace Features.DependencyInjection.Main
{
    public class MainDependencyInjector : IDependencyInjector
    {
        private readonly List<IService> _services;
        private static GameObject _gameObject;

        public MainDependencyInjector(GameObject singletonObject)
        {
            _services = new List<IService>();
            _gameObject = singletonObject;
        }

        public void Initialize()
        {
            CreateDependencies();
        }

        public T GetDependency<T>() where T : IService
        {
            foreach (var service in _services)
            {
                if (service is T serviceOfTypeT)
                {
                    return serviceOfTypeT;
                }
            }

            return default;
        }

        private void CreateDependencies()
        {
            Bind<IConfigProviderService>(new ConfigProviderService());
            Bind<IService>(new EventService());
        }

        private void Bind<TBind>(TBind instance) where TBind : IService
        {
            _services.Add(instance);
        }
    }
}