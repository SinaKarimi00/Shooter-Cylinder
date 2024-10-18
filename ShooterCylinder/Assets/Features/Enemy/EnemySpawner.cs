using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Enemy.Config;
using Features.MainScript.Application;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Enemy
{
    public class EnemySpawner : IUpdater
    {
        private readonly float _spawnInterval;
        private readonly Enemy _enemyPrefab;
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private float _nextSpawnTime;
        private int _enemiesPerSpawn;
        private readonly float _initialSpawnInterval;
        private readonly float _difficultyRampTime;
        private readonly float _minimumSpawnInterval;
        private readonly float _maxEnemiesPerSpawn;

        public EnemySpawner()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            var enemyContainer = configProviderService.GetConfig<EnemyContainer>();
            _enemySpawnerConfig = enemyContainer.EnemySpawnerConfig;
            _initialSpawnInterval = _enemySpawnerConfig.InitialSpawnInterval;
            _enemyPrefab = enemyContainer.EnemyPrefab;
            var initialEnemiesPerSpawn = _enemySpawnerConfig.InitialEnemiesPerSpawn;
            _nextSpawnTime = Time.time + initialEnemiesPerSpawn;
            _difficultyRampTime = _enemySpawnerConfig.DifficultyRampTime;
            _minimumSpawnInterval = _enemySpawnerConfig.MinimumSpawnInterval;
            _maxEnemiesPerSpawn = _enemySpawnerConfig.MaxEnemiesPerSpawn;
        }

        public void Update()
        {
            if (Time.time >= _nextSpawnTime)
            {
                SpawnEnemy();
                UpdateSpawnSettings();
                _nextSpawnTime = Time.time + GetCurrentSpawnInterval();
            }
        }

        private void SpawnEnemy()
        {
            var canSelectHole = _enemySpawnerConfig.TryToGetRandomSpawnerHole(out var hole);
            if (canSelectHole)
            {
                Object.Instantiate(_enemyPrefab, hole.position, Quaternion.identity);
            }
        }

        private void UpdateSpawnSettings()
        {
            if (_enemiesPerSpawn < _maxEnemiesPerSpawn)
            {
                _enemiesPerSpawn++;
            }
        }

        private float GetCurrentSpawnInterval()
        {
            var elapsedTime = Time.time;
            var progress = Mathf.Clamp01(elapsedTime / _difficultyRampTime);
            return Mathf.Lerp(_initialSpawnInterval, _minimumSpawnInterval, progress);
        }
    }
}