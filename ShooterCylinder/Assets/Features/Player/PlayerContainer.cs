using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Player.Configs;
using Features.Player.Shooting;
using UnityEngine;

namespace Features.Player
{
    public class PlayerContainer : MonoBehaviour, IConfig
    {
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform popupCanvas;
        private ObjectPool _bulletPool;
        private PlayerConfig _playerConfig;


        private void Awake()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            configProviderService.RegisterConfig(this);
            _playerConfig = Resources.Load<PlayerConfig>("Player/PlayerConfig");
            _bulletPool = new ObjectPool();
        }

        public Rigidbody PlayerRigidbody => playerRigidbody;
        public Transform PlayerTransform => playerTransform;
        public PlayerConfig PlayerConfig => _playerConfig;
        public ObjectPool ObjectPool => _bulletPool;
        public Transform FirePoint => firePoint;
        public Transform PopupCanvas => popupCanvas;
    }
}