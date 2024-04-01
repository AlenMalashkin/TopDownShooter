using Code.Services.EquipmentService;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Code.GameplayLogic.PlayerLogic
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : AnimatorComponent
    {
        private bool _isMoving;
        private bool _isMovingBackwards;

        private Transform _cameraTransform;
        private IInputService _inputService;
        private IEquipmentService _equipmentService;

        public void Init(IEquipmentService equipmentService, IInputService inputService, Transform cameraTransform)
        {
            _inputService = inputService;
            _equipmentService = equipmentService;
            _cameraTransform = cameraTransform;
        }

        private void Start()
        {
            _inputService.GetInputAction<IMovementAction>().SubscribeMovementInput(OnMove);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<IMovementAction>().UnsubscribeMovementInput(OnMove);
        }

        public void PlayDeathAnimation()
        {
            SetLayerWeight(GetLayerIndex("Upper Body"), 0f);
            PlayAnimationByName(AnimationStrings.Death, GetLayerIndex("Base Layer"));
        }

        public void PlayShootAnimation()    
            => PlayAnimationByName(GetShootAnimationName(_equipmentService.CurrentWeaponCategory),
                GetLayerIndex("Upper Body"));

        public void PlayRunWithWeaponAnimation()
            => PlayAnimationByName(GetRunWithWeaponAnimationName(_equipmentService.CurrentWeaponCategory),
                GetLayerIndex("Upper Body"));

        public void PlayReloadAnimation()
            => PlayAnimationByName("Reload", GetLayerIndex("Upper Body"));

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

            SetFloat(AnimationStrings.Horizontal, relativeVector.x);
            SetFloat(AnimationStrings.Vertical, relativeVector.z);
        }

        private string GetShootAnimationName(WeaponCategory category)
            => AnimationStrings.WeaponShootAnimationNames[category];

        private string GetRunWithWeaponAnimationName(WeaponCategory category)
            => AnimationStrings.RunWithWeaponAnimationNames[category];
    }
}