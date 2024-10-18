using UnityEngine;

namespace Features.Enemy.Config
{
    [CreateAssetMenu(menuName = "Game/Enemy/EnemyConfig", fileName = "New Enemy config")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float enemyMoveSpeed;

        public float EnemyMoveSpeed => enemyMoveSpeed;
     
    }
}