using Code.Services;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector3 _moveDirection;
        private CharacterController _controller;
        private IInputService _inputService;


        public void Init(IInputService inputService)
        {
            _inputService = inputService;
            
            _controller = GetComponent<CharacterController>();
            _inputService = ServiceLocator.Container.Resolve<IInputService>();
        }

        private void Start()
        {
            _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
        }

        private void Update()
        {
            _controller.Move(_moveDirection * _speed * Time.deltaTime);
            
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }
}