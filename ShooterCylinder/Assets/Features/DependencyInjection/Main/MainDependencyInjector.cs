using System.Collections.Generic;
using Features.DependencyInjection.Application;
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
        }

        private void Bind<TBind>(TBind instance) where TBind : IService
        {
            _services.Add(instance);
        }
    }
}