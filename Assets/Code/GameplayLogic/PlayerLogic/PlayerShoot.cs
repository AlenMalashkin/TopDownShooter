using System;
using Code.Audio;
using Code.GameplayLogic.Weapons;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using GamePush;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        public bool IsShooting => _isShooting;
        public bool IsReloading => _weapon.Reloading;

        private bool _isShooting;
        private float _fireDelayCounter;
        private IInputService _inputService;
        private Joystick _fireJoystick;
        private Button _reloadButton;
        private Weapon _weapon;

        public void Init(IInputService inputService, Weapon weapon)
        {
            _inputService = inputService;
            _weapon = weapon;
            _inputService.GetInputAction<IReloadAction>().SubscribeReloadAction(OnReload);
        }

        public void Init(Joystick fireJoystick, Weapon weapon, Button reloadButton)
        {
            _fireJoystick = fireJoystick;
            _weapon = weapon;
            _reloadButton = reloadButton;
            _reloadButton.onClick.AddListener(_weapon.Reload);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IReloadAction>().SubscribeReloadAction(OnReload);
            
            if (GP_Device.IsMobile())
                _reloadButton.onClick.RemoveListener(_weapon.Reload);
        }

        public void PlayReloadSound()
            => _soundPlayer.PlaySoundEffect(_reloadSound);

        private void Update()
        {
            if (_weapon.Reloading)
            {
                _playerAnimator.PlayReloadAnimation();
                return;
            }
            
            if (!GP_Device.IsMobile())
            {
                if (_inputService.GetInputAction<IFireAction>().FirePressed)
                {
                    Shoot();
                    _isShooting = true;
                }
                else
                {
                    _isShooting = false;
                }
            }
            else
            {
                if (_fireJoystick.Direction != Vector2.zero)
                {
                    Shoot();
                    _isShooting = true;
                }
                else
                {
                    _isShooting = false;
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
                _weapon.Reload();
            }
        }

        private void OnReload(InputAction.CallbackContext context)
        {
            _weapon.Reload();
        }
    }
}