using UnityEngine;

namespace Features.Player.Configs
{
    [CreateAssetMenu(menuName = "Game/Player/PlayerConfig", fileName = "New player config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private int poolSize;

        public float MoveSpeed => moveSpeed;
        public int PoolSize => poolSize;
    }
}