using Code.Services;
using Code.Services.InputService;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject _shootingPoint;

        private float _range = 100f;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = ServiceLocator.Container.Resolve<IInputService>();
            Debug.Log(_inputService);
        }

        private void OnEnable()
        {
            _inputService.SubscribeFireInput(OnFire);
        }

        private void OnDisable()
        {
            _inputService.UnsubscribeFireInput(OnFire);
        }
        
        private void OnFire(InputAction.CallbackContext context)
        {
            Fire();
        }

        private void Fire()
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

    }
}