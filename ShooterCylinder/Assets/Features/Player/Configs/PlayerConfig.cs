using UnityEngine;

namespace Features.Player.Configs
{
    [CreateAssetMenu(menuName = "Game/Player/PlayerConfig", fileName = "New player config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private float mouseSensitivity;
        [SerializeField] private int poolSize;

        public float MoveSpeed => moveSpeed;
        public float MouseSensitivity => mouseSensitivity;
        public int PoolSize => poolSize;
    }
}