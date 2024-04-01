using Code.GameplayLogic.Weapons;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator playerAnimator;
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
                playerAnimator.PlayRunWithWeaponAnimation();
        }

        private void Shoot()
        {
            if (_weapon.CanShoot)
            {
                _weapon.ShootBullet(transform.forward);
                playerAnimator.PlayShootAnimation();
            }
            else
            {
                _weapon.Reload();
                playerAnimator.PlayReloadAnimation();
            }
        }
    }
}