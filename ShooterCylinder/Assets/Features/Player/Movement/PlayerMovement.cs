using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.Main.Application;
using UnityEngine;

namespace Features.Player.Movement
{
    public class PlayerMovement : IUpdater, IFixedUpdater
    {
        private readonly Rigidbody _rb;
        private readonly float _moveSpeed;
        private float _horizontalInput;
        private float _verticalInput;
        private float _mouseX;
        private float _xRotation;
        private Vector3 _moveDirection;

        public PlayerMovement()
        {
            Cursor.lockState = CursorLockMode.Locked;
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            var playerContainer = configProviderService.GetConfig<PlayerContainer>();
            _rb = playerContainer.PlayerRigidbody;
            _moveSpeed = playerContainer.PlayerConfig.MoveSpeed;
        }

        public void Update()
        {
            MovePlayer();
        }

        public void FixedUpdate()
        {
            MyInput();
        }


        private void MyInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void MovePlayer()
        {
            _moveDirection = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
            _rb.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Force);
        }
    }
}