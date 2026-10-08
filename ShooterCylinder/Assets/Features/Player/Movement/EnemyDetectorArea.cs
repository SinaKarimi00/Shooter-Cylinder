using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using UnityEngine;

namespace Features.Player.Movement
{
    public class EnemyDetectorArea : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private PlayerContainer _playerContainer;

        private void Start()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            _playerContainer = configProviderService.GetConfig<PlayerContainer>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;
            var direction = other.transform.position - _playerContainer.PlayerTransform.position;
            var targetRotation = Quaternion.LookRotation(direction);
            var lookAt = Quaternion.RotateTowards(_playerContainer.PlayerTransform.rotation, targetRotation,
                Time.deltaTime * rotationSpeed);
            _playerContainer.PlayerTransform.rotation = lookAt;
        }
    }
}