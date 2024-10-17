using UnityEngine;

namespace Features.DependencyInjection.Main
{
    public class MonoDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            if (DependencyInjector.Instance == null)
            {
                var singleton = new GameObject("Dependency Injection");
                DontDestroyOnLoad(singleton);
                DependencyInjector.Initialize(new MainDependencyInjector(singleton));
            }
        }
    }
}