using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Main.Application;
using UnityEngine;

namespace Features.Player.Shooting
{
    public class Shoot : IUpdater
    {
        private readonly ObjectPool _bulletPool;
        private readonly Transform _firePoint;

        public Shoot()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            var playerContainer = configProviderService.GetConfig<PlayerContainer>();
            _bulletPool = playerContainer.ObjectPool;
            _firePoint = playerContainer.FirePoint;
        }


        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shooting();
            }
        }

        private void Shooting()
        {
            var existFreeBullet = _bulletPool.TryToGetPooledBullet(out var bullet);

            if (!existFreeBullet)
            {
                return;
            }

            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.SetActive(true);

            var bulletComponent = bullet.GetComponent<Bullet>();
            var shootDirection = _firePoint.forward;
            bulletComponent.MoveBullet(shootDirection);
        }
    }
}