using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector3 _moveDirection;
        private IInputService _inputService;
        private Rigidbody _rigidbody;


        public void Init(IInputService inputService)
        {
            _inputService = inputService;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _moveDirection * _speed;
        }
        

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }
}