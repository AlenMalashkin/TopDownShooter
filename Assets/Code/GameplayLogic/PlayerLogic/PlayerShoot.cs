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
        [SerializeField] private int _damage = 25;
        
        public Transform PlayerArm => playerArm;

        private float _shootingCooldown;
        private IInputService _inputService;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Shoot()
        {
            RaycastHit hit;
            
            if (Physics.Raycast(_shootingPoint.transform.position, _shootingPoint.transform.forward, out hit, _range))
            {
                IDamageable damageable = hit.transform.GetComponent<Damageable>();
                
                if (damageable != null)
                {
                    damageable.TakeDamage(_damage);
                }
                else
                {
                    Debug.Log("pizdec");
                    Debug.Log(hit.transform.name);
                }
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
                if (_shootingCooldown > 0.5)
                {
                    _playerAnimator.PlayShootAnimation();
                    Shoot();
                    _shootingCooldown = 0;
                }
            }
            else
            {
                _playerAnimator.PlayRunWithWeaponAnimation();
            }

            _shootingCooldown += Time.deltaTime;
        }
    }
}