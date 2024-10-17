using Features.DependencyInjection.Application;

namespace Features.DependencyInjection.Main
{
    public static class DependencyInjector
    {
        public static IDependencyInjector Instance { get; set; }

        public static void Initialize(IDependencyInjector instance)
        {
            Instance = instance;
            Instance.Initialize();
        }
    }
}