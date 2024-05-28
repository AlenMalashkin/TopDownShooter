using System;
using Code.Level;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using GamePush;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public Vector2 MoveDirection => _moveDirection;
        
        private Joystick _movementJoystick;
        private Vector3 _moveDirection;
        private IInputService _inputService;
        private Rigidbody _rigidbody;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Joystick movementJoystick)
        {
            _movementJoystick = movementJoystick;
        }

        private void Start()
        {
            if (GP_Device.IsDesktop())
                _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            if (GP_Device.IsDesktop())
                _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
            
            _rigidbody.velocity = Vector3.zero;
        }

        private void Update()
        {
            _moveDirection = new Vector3(_movementJoystick.Direction.x, 0, _movementJoystick.Direction.y);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _moveDirection * _speed;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }
}