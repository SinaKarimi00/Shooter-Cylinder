using System.Collections.Generic;
using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using UnityEngine;

namespace Features.Player.Shooting
{
    public class ObjectPool
    {
        private readonly List<GameObject> _bulletPool;

        public ObjectPool()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            var playerContainer = configProviderService.GetConfig<PlayerContainer>();
            var bulletPrefab = Resources.Load<GameObject>("Player/Bullet");
            _bulletPool = new List<GameObject>();

            for (var i = 0; i < playerContainer.PlayerConfig.PoolSize; i++)
            {
                var bullet = Object.Instantiate(bulletPrefab);
                bullet.SetActive(false);
                _bulletPool.Add(bullet);
            }
        }

        public bool TryToGetPooledBullet(out GameObject selectedBullet)
        {
            var existFreeBullet = false;
            selectedBullet = default;

            foreach (var bullet in _bulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    selectedBullet = bullet;
                    existFreeBullet = true;
                    break;
                }
            }

            return existFreeBullet;
        }
    }
}