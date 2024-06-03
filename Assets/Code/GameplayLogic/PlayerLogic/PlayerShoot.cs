using Code.GameplayLogic.Weapons;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using GamePush;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _playerArm;

        public Transform PlayerArm => _playerArm;

        private IInputService _inputService;
        private Joystick _fireJoystick;
        private Weapon _weapon;

        public void Init(IInputService inputService, Weapon weapon)
        {
            _inputService = inputService;
            _weapon = weapon;
        }

        private void Update()
        {
            if (!GP_Device.IsMobile())
            {
                if (_inputService.GetInputAction<IFireAction>().FirePressed)
                    Shoot();
                else
                    _playerAnimator.PlayRunWithWeaponAnimation();
            }
            else
            {
                if (_fireJoystick.Direction != Vector2.zero)
                    Shoot();
                else
                    _playerAnimator.PlayRunWithWeaponAnimation();
            }
        }

        public void Init(Joystick fireJoystick, Weapon weapon)
        {
            _fireJoystick = fireJoystick;
            _weapon = weapon;
        }

        private void Shoot()
        {
            if (_weapon.CanShoot)
            {
                _weapon.Shoot(transform.forward);
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