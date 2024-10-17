using UnityEngine;

namespace Features.Player.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 200f;
        public float maxLifetime = 5f;
        private float _lifetime;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _lifetime = 0f;
            _rb.velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            _lifetime += Time.fixedDeltaTime;

            if (_lifetime > maxLifetime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
        }

        public void MoveBullet(Vector3 direction)
        {
            _rb.AddForce(direction * speed, ForceMode.Force);
        }
    }
}