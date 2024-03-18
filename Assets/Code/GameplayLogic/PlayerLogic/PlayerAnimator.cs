using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    [RequireComponent(typeof(PlayerMovement), typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private bool _isMoving;
        private bool _isMovingBackwards;

        private Transform _cameraTransform;
        private IInputService _inputService;
        
        public void Init(IInputService inputService, Transform cameraTransform)
        {
            _inputService = inputService;
            _cameraTransform = cameraTransform;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            UpdateMovementAnimations(context.ReadValue<Vector2>());
        }

        private void UpdateMovementAnimations(Vector2 direction)
        {
            Vector3 cameraRight = _cameraTransform.right;
            Vector3 cameraForward = _cameraTransform.forward;

            cameraRight.y = 0;
            cameraForward.y = 0;

            Vector3 movementVector = cameraForward.normalized * direction.y
                                     + cameraRight.normalized * direction.x;
            movementVector = Vector3.ClampMagnitude(movementVector, 1);

            Vector3 relativeVector = transform.InverseTransformDirection(movementVector);

            _animator.SetFloat(AnimationStrings.Horizontal, relativeVector.x);
            _animator.SetFloat(AnimationStrings.Vertical, relativeVector.z);
        }
    }
}