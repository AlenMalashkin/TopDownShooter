using Code.GameplayLogic.Weapons;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform playerArm;

        public Transform PlayerArm => playerArm;

        private IInputService _inputService;
        private Weapon _weapon;

        public void Init(IInputService inputService, Weapon weapon)
        {
            _inputService = inputService;
            _weapon = weapon;
        }

        private void Update()
        {
            if (_inputService.GetInputAction<IFireAction>().FirePressed)
                Shoot();
            else
                _playerAnimator.PlayRunWithWeaponAnimation();
        }

        private void Shoot()
        {
            if (_weapon.CanShoot)
            {
                _weapon.ShootBullet(transform.forward);
                _playerAnimator.PlayShootAnimation();
            }
            else
            {
                _weapon.Reload();
                _playerAnimator.PlayReloadAnimation();
            }
        }
    }
}