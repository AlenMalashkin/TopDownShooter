using Code.GameplayLogic.PlayerLogic;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform playerArm;
        public Transform PlayerArm => playerArm;
        
        private IWeapon _weapon;
        private IInputService _inputService;

        public void Init(IWeapon weapon, IInputService inputService)
        {
            _weapon = weapon;
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService.GetInputAction<IFireAction>().FirePressed)
            {
                _weapon.Shoot(Vector3.forward);
                _playerAnimator.PlayShootAnimation();
            }
            else
            {
                _playerAnimator.PlayRunWithWeaponAnimation();
            }
        }
    }
}