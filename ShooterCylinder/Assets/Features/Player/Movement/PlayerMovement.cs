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
        private readonly float _mouseSensitivity;
        private float _xRotation;
        private Vector3 _moveDirection;
        private readonly PlayerContainer _playerContainer;

        public PlayerMovement()
        {
            Cursor.lockState = CursorLockMode.Locked;
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            _playerContainer = configProviderService.GetConfig<PlayerContainer>();
            _rb = _playerContainer.PlayerRigidbody;
            _moveSpeed = _playerContainer.PlayerConfig.MoveSpeed;
            _mouseSensitivity = _playerContainer.PlayerConfig.MouseSensitivity;
        }

        public void Update()
        {
            MovePlayer();
        }

        public void FixedUpdate()
        {
            MyInput();
            RotatePlayer();
        }


        private void MyInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
            _mouseX = Input.GetAxisRaw("Mouse X");
        }

        private void MovePlayer()
        {
            _moveDirection = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
            _rb.AddForce(_moveDirection.normalized * (_moveSpeed * 10f), ForceMode.Force);
        }

        private void RotatePlayer()
        {
            _mouseX *= _mouseSensitivity;
            _playerContainer.PlayerTransform.Rotate(Vector3.up * _mouseX);
        }
    }
}