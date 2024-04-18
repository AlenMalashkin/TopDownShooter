using System;
using System.Collections;
using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _destroyTime;

        private int _damage;
        private Vector3 _direction;
        private Rigidbody _rigidbody;

        public void Init(int damage, Vector3 direction)
        {
            _damage = damage;
            _direction = direction;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(DestroyBulletRoutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            MoveBullet();
        }

        private void MoveBullet() =>
            _rigidbody.velocity = _direction * _bulletSpeed;

        private IEnumerator DestroyBulletRoutine()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}