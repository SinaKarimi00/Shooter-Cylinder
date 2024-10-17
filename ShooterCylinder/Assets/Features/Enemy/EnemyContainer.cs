using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Enemy.Config;
using UnityEngine;

namespace Features.Enemy
{
    public class EnemyContainer : MonoBehaviour, IConfig
    {
        private EnemyConfig _enemyConfig;
        private EnemySpawnerConfig _enemySpawnerConfig;
        private Enemy _enemyPrefab;

        private void Awake()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            configProviderService.RegisterConfig(this);
            _enemyConfig = Resources.Load<EnemyConfig>("Enemy/EnemyConfig");
            _enemySpawnerConfig = Resources.Load<EnemySpawnerConfig>("Enemy/EnemySpawnerConfig");
            _enemyPrefab = Resources.Load<Enemy>("Enemy/Enemy");
        }

        public EnemyConfig EnemyConfig => _enemyConfig;
        public Enemy EnemyPrefab => _enemyPrefab;
        public EnemySpawnerConfig EnemySpawnerConfig => _enemySpawnerConfig;
    }
}