using System;
using System.Collections;
using Code.Data;
using Code.Factories.GameplayFactoies;
using UnityEngine;

namespace Code.GameplayLogic.Weapons.PlayerWeapons
{
    public class PlayerWeapon : Weapon
    {
        public override event Action<int> AmmoChanged;

        [SerializeField] private Vector3 _weaponPositionInHand = Vector3.zero;
        [SerializeField] private Vector3 _weaponRotationInHand;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _maxBullets;
        [SerializeField] private int _damage;
        [SerializeField] private Transform _shootPoint;

        public override bool CanShoot => !IsClipEmpty && !_isReloading;

        private bool IsClipEmpty => _bulletsInClip == 0;

        private Bullet _bulletPrefab;
        private bool _isReloading;
        private float _shootCooldown;
        private int _bulletsInClip;
        private IWeaponFactory _weaponFactory;

        public int BulletsInClip => _bulletsInClip;

        private void Start()
        {
            AmmoChanged?.Invoke(_bulletsInClip);
        }

        public void Init(IWeaponFactory gameFactory, Bullet bulletPrefab)
        {
            _weaponFactory = gameFactory;
            _bulletPrefab = bulletPrefab;
            _bulletsInClip = _maxBullets;
        }

        private void Update()
        {
            _shootCooldown += Time.deltaTime;
        }

        public override void AttachToHand(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = _weaponPositionInHand;
            transform.localRotation = Quaternion.Euler(_weaponRotationInHand.x,
                _weaponRotationInHand.y, _weaponRotationInHand.z);
        }

        public override void ShootBullet(Vector3 shootDirection)
        {
            if (_shootCooldown > _fireRate)
            {
                Bullet createdBullet = _weaponFactory.CreateBullet(_shootPoint.position, _bulletPrefab);
                createdBullet.Init(_damage, shootDirection);
                _bulletsInClip--;
                _shootCooldown = 0f;

                AmmoChanged?.Invoke(_bulletsInClip);
            }
        }

        public override void Reload()
        {
            StartCoroutine(ReloadingRoutine());
        }

        private IEnumerator ReloadingRoutine()
        {
            _isReloading = true;
            yield return new WaitForSeconds(_reloadTime);
            _bulletsInClip = _maxBullets;
            _isReloading = false;

            AmmoChanged?.Invoke(_bulletsInClip);
        }
    }
}