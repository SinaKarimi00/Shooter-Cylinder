namespace Features.DependencyInjection.Application
{
    public interface IDependencyInjector
    {
        public void Initialize();
        public T GetDependency<T>() where T : IService;
    }
}