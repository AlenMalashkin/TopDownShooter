using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using GamePush;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private PlayerShoot _playerShoot;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Rigidbody _rigidbody;

        public Vector2 MoveDirection => new Vector2(_moveDirection.x, _moveDirection.z);

        private Joystick _movementJoystick;
        private Vector3 _moveDirection;
        private IInputService _inputService;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Init(Joystick movementJoystick)
        {
            _movementJoystick = movementJoystick;
        }

        private void Start()
        {
            if (!GP_Device.IsMobile())
                _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            if (!GP_Device.IsMobile())
                _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
            
            _rigidbody.velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (GP_Device.IsMobile())
                _moveDirection = new Vector3(_movementJoystick.Direction.x, 0, _movementJoystick.Direction.y);

            if (_moveDirection == Vector3.zero && !_playerShoot.IsShooting && !_playerShoot.IsReloading)
                _playerAnimator.PlayAnimationByName("Idle");
            
            if (_moveDirection != Vector3.zero && !_playerShoot.IsShooting && !_playerShoot.IsReloading)
                _playerAnimator.PlayRunWithWeaponAnimation();

            _rigidbody.velocity = _moveDirection * _speed;
        }


        private void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }
}