using System;
using Features.DependencyInjection.Application;

namespace Features.ConfigProvider
{
    public interface IConfigProviderService : IService
    {
        public IConfig GetConfig(Type type);
        public T GetConfig<T>() where T : IConfig;
        public void RegisterConfig(IConfig config);
        public void UnRegisterConfigsOfType<T>() where T : IConfig;
        public void UnRegisterConfig(IConfig config);
        public void UnRegisterAll();
    }
}