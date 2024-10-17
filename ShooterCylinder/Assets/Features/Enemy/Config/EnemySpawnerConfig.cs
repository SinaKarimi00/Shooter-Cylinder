using System.Collections.Generic;
using UnityEngine;

namespace Features.Enemy.Config
{
    [CreateAssetMenu(menuName = "Game/Enemy/EnemySpawnerConfig", fileName = "New Enemy spawner config")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [SerializeField] private List<SpawnerHoleData> spawnerHoles;
        [SerializeField] private float initialSpawnInterval = 5f;
        [SerializeField] private float difficultyRampTime = 60f;
        [SerializeField] private float minimumSpawnInterval = 1f;
        [SerializeField] private int initialEnemiesPerSpawn = 1;
        [SerializeField] private int maxEnemiesPerSpawn = 5;


        public float InitialSpawnInterval => initialSpawnInterval;
        public float DifficultyRampTime => difficultyRampTime;
        public float MinimumSpawnInterval => minimumSpawnInterval;
        public float MaxEnemiesPerSpawn => maxEnemiesPerSpawn;
        public int InitialEnemiesPerSpawn => initialEnemiesPerSpawn;


        public bool TryToGetRandomSpawnerHole(out SpawnerHoleData spawnerHole)
        {
            var totalRate = 0;
            foreach (var hole in spawnerHoles)
            {
                totalRate += hole.spawnRate;
            }

            var randomValue = Random.Range(0, totalRate);
            var currentRateSum = 0;

            foreach (var hole in spawnerHoles)
            {
                currentRateSum += hole.spawnRate;
                if (randomValue < currentRateSum)
                {
                    spawnerHole = hole;
                    return true;
                }
            }

            spawnerHole = default;
            return false;
        }
    }
}