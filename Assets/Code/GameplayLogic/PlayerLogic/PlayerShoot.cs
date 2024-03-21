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
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private float _range = 1000f;
        public Transform PlayerArm => playerArm;
        
        private IInputService _inputService;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Shoot(Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(_shootingPoint.transform.position, _shootingPoint.transform.forward, out hit, _range))
            {
                Debug.Log(hit.transform.name);
            }   
            else
            {
                Debug.Log("не попал");
            }
        }
        
        private void Update()
        {
            if (_inputService.GetInputAction<IFireAction>().FirePressed)
            {
                _playerAnimator.PlayShootAnimation();
                Shoot(transform.forward);
            }
            else
            {
                _playerAnimator.PlayRunWithWeaponAnimation();
            }
        }
    }
}