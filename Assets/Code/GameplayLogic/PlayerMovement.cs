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
        private Animator _animator;
        private Transform _playerDirection;
        private bool _isMoving;
        private bool _isMovingBackwards;

        public bool IsMoving
        {
            get => _isMoving;
            private set
            {
                _isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }

        public bool IsMovingBackwards
        {
            get => _isMovingBackwards;
            private set
            {
                _isMovingBackwards = value;
                _animator.SetBool(AnimationStrings.IsMovingBackwards, value);
            }
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputService = ServiceLocator.Container.Resolve<IInputService>();
            _animator = GetComponentInChildren<Animator>();
            _playerDirection = GetComponentInParent<Transform>();
        }

        private void OnEnable()
        {
            _inputService.Enable();
            _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            _inputService.Disable();
            _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
        }

        private void Update()
        {
            _controller.Move(_moveDirection * _speed * Time.deltaTime);
            IsMoving = _moveDirection != Vector3.zero;
            IsMovingBackwards = _moveDirection.z < 0;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }
}