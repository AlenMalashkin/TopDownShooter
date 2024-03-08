using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Transform playerArm;
        public Transform PlayerArm => playerArm;
        
        private IWeapon _weapon;
        private IInputService _inputService;

        public void Init(IWeapon weapon, IInputService inputService)
        {
            _weapon = weapon;
            _inputService = inputService;
        }
        
        private void Start()
        {
            _inputService.GetInputAction<IFireAction>().SubscribeFireInput(OnFire);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IFireAction>().UnsubscribeFireInput(OnFire);
        }
        
        private void OnFire(InputAction.CallbackContext context)
        {
            _weapon.Shoot(Vector3.forward);
        }
    }
}