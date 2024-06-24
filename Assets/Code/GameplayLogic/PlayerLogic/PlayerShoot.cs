using Code.Audio;
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
        [SerializeField] private float _fireDelay = 0.1f;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private AudioClip _reloadSound;

        public Transform PlayerArm => _playerArm;

        private float _fireDelayCounter;
        private IInputService _inputService;
        private Joystick _fireJoystick;
        private Weapon _weapon;

        public void Init(IInputService inputService, Weapon weapon)
        {
            _inputService = inputService;
            _weapon = weapon;
        }

        public void Init(Joystick fireJoystick, Weapon weapon)
        {
            _fireJoystick = fireJoystick;
            _weapon = weapon;
        }

        public void PlayReloadSound()
            => _soundPlayer.Play(_reloadSound);

        private void Update()
        {
            if (!GP_Device.IsMobile())
            {
                if (_inputService.GetInputAction<IFireAction>().FirePressed)
                {
                    Shoot();
                }
                else
                {
                    _playerAnimator.PlayRunWithWeaponAnimation();
                }
            }
            else
            {
                if (_fireJoystick.Direction != Vector2.zero)
                {
                    Shoot();
                }
                else
                {
                    _playerAnimator.PlayRunWithWeaponAnimation();
                }
            }
        }

        private void Shoot()
        {
            if (_weapon.CanShoot)
            {
                _playerAnimator.PlayShootAnimation();
                _fireDelayCounter += Time.deltaTime;

                if (_fireDelayCounter > _fireDelay)
                {
                    _weapon.Shoot(transform.forward);
                    _fireDelayCounter = 0f;
                }
            }
            else
            {
                _playerAnimator.PlayReloadAnimation();
                _weapon.Reload();
            }
        }
    }
}