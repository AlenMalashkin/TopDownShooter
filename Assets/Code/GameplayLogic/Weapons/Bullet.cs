using System;
using System.Collections;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _destroyTime;

        private int _damage;
        private Vector3 _direction;

        public void Init(int damage, Vector3 direction)
        {
            _damage = damage;
            _direction = direction;
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
        }

        private void Update()
        {
            transform.position += _direction * (_bulletSpeed * Time.deltaTime);
        }

        private IEnumerator DestroyBulletRoutine()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}