using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Enemy.Config;
using UnityEngine;

namespace Features.Enemy
{
    public class EnemyContainer : MonoBehaviour, IConfig
    {
        private EnemyConfig _enemyConfig;

        private void Awake()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            configProviderService.RegisterConfig(this);
            _enemyConfig = Resources.Load<EnemyConfig>("Enemy/EnemyConfig");
        }

        public EnemyConfig EnemyConfig => _enemyConfig;
    }
}