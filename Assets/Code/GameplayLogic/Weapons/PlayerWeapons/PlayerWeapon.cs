using System;
using System.Collections;
using Code.Audio;
using Code.Factories.GameplayFactoies;
using Code.Services.InputService;
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
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _shotSound;
        [SerializeField] private AudioClip _reloadSound;

        public int BulletsInClip => _bulletsInClip;
        public override bool CanShoot => !IsClipEmpty && !_isReloading;
        protected Transform ShootPoint => _shootPoint;
        protected IWeaponFactory WeaponFactory => _weaponFactory;
        protected Bullet BulletPrefab => _bulletPrefab;
        private bool IsClipEmpty => _bulletsInClip == 0;

        private Bullet _bulletPrefab;
        private bool _isReloading;
        private float _shootCooldown;
        private int _bulletsInClip;
        private IWeaponFactory _weaponFactory;
        private IInputService _inputService;

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

        public override void Shoot(Vector3 shootDirection)
        {
            if (_shootCooldown > _fireRate)
            {
                ShootBullets(shootDirection, _damage);
                _particleSystem.Play();
                _soundPlayer.PlaySoundEffect(_shotSound);
                _bulletsInClip--;
                _shootCooldown = 0f;

                AmmoChanged?.Invoke(_bulletsInClip);
            }
        }

        public override void ShootBullets(Vector3 direction, int damage)
        {
            Bullet createdBullet = _weaponFactory.CreateBullet(_shootPoint.position, _bulletPrefab);
            createdBullet.transform.rotation = Quaternion.LookRotation(direction);
            createdBullet.Init(damage, direction);
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