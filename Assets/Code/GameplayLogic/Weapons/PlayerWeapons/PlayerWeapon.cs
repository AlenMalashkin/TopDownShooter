using System.Collections;
using Code.Factories.GameplayFactoies;
using UnityEngine;

namespace Code.GameplayLogic.Weapons.PlayerWeapons
{
    public class PlayerWeapon : Weapon
    {
        [SerializeField] private Vector3 _weaponPositionInHand = Vector3.zero;
        [SerializeField] private Vector3 _weaponRotationInHand;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _maxBullets;
        [SerializeField] private int _damage;
        [SerializeField] private Transform _shootPoint;

        public override bool CanShoot => !IsClipEmpty && !_isReloading;
        private bool IsClipEmpty => _bulletsInClip == 0;

        private bool _isReloading;
        private float _shootCooldown;
        private int _bulletsInClip;
        private IGameFactory _gameFactory;
        
        public void Init(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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
                _gameFactory.CreateBullet(_shootPoint.position, _damage, shootDirection);
                _bulletsInClip--;
                _shootCooldown = 0f;
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
        }
    }
}