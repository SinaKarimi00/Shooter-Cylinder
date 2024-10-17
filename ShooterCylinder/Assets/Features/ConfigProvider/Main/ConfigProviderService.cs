using System;
using System.Collections.Generic;
using System.Linq;

namespace Features.ConfigProvider.Main
{
    public class ConfigProviderService : IConfigProviderService
    {
        private readonly List<IConfig> _configs = new();

        public IConfig GetConfig(Type type)
        {
            var config = _configs.FirstOrDefault(type.IsInstanceOfType);
            return config;
        }

        public T GetConfig<T>() where T : IConfig
        {
            foreach (var config in _configs)
            {
                if (config is T configOfTypeT)
                {
                    return configOfTypeT;
                }
            }

            return default;
        }

        public void RegisterConfig(IConfig config)
        {
            var typeOfIConfig = config.GetType();
            var configForThisTypeExist = DoesConfigForThisTypeExist(typeOfIConfig, out var previousConfigWithSameType);
            if (configForThisTypeExist)
            {
                var index = _configs.IndexOf(previousConfigWithSameType);
                _configs[index] = config;
                return;
            }

            if (!_configs.Contains(config))
            {
                _configs.Add(config);
            }
        }

        public void UnRegisterConfigsOfType<T>() where T : IConfig
        {
            var configNeedToBeRemoved = new List<IConfig>();
            foreach (var config in _configs)
                if (config is T)
                    configNeedToBeRemoved.Add(config);

            foreach (var config in configNeedToBeRemoved)
            {
                _configs.Remove(config);
            }
        }


        public void UnRegisterAll()
        {
            _configs.Clear();
        }

        public void UnRegisterConfig(IConfig config)
        {
            RemoveConfig(config);
        }

        private void RemoveConfig(IConfig config)
        {
            if (_configs.Contains(config))
                _configs.Remove(config);
        }

        private bool DoesConfigForThisTypeExist(Type typeOfIConfig, out IConfig foundedConfig)
        {
            foreach (var config in _configs)
            {
                if (config.GetType() == typeOfIConfig)
                {
                    foundedConfig = config;
                    return true;
                }
            }

            foundedConfig = null;
            return false;
        }
    }
}