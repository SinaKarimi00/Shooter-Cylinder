using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Player;
using UnityEngine;

namespace Features.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private PlayerContainer _playerContainer;
        private EnemyContainer _enemyContainer;
        private float _moveSpeed;

        private void Start()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            _playerContainer = configProviderService.GetConfig<PlayerContainer>();
            _enemyContainer = configProviderService.GetConfig<EnemyContainer>();
            _moveSpeed = _enemyContainer.EnemyConfig.EnemyMoveSpeed;
        }

        private void FixedUpdate()
        {
            FollowingPlayer();
        }

        private void FollowingPlayer()
        {
            var enemyDirection = _playerContainer.PlayerTransform.position;
            transform.position = Vector3.MoveTowards(transform.position, enemyDirection, _moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag($"Bullet"))
            {
                Destroy(gameObject, .1f);
            }
        }
    }
}