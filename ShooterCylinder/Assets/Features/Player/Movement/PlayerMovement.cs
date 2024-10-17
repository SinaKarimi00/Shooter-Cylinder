using UnityEngine;

namespace Features.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public float moveSpeed;
        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _moveDirection;

        private void Update()
        {
            MyInput();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }


        private void MyInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void MovePlayer()
        {
            _moveDirection = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
            rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        }
    }
}