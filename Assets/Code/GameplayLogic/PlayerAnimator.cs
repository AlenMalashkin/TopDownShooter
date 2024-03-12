using UnityEngine;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Transform _mainCamera;
        private Animator _animator;
        private bool _isMoving;
        private bool _isMovingBackwards;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainCamera = Camera.main.transform;
        }
        
        
        private void Update()
        {
            CalculateMovementVector();
        }

        private void CalculateMovementVector()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        
            Vector3 cameraRight = _mainCamera.right;
            Vector3 cameraForward = _mainCamera.forward;

            cameraRight.y = 0;
            cameraForward.y = 0;

            Vector3 movementVector = cameraForward.normalized * vertical + cameraRight.normalized * horizontal;
            movementVector = Vector3.ClampMagnitude(movementVector, 1);
            
            Vector3 relativeVector = transform.InverseTransformDirection(movementVector);
            
            _animator.SetFloat(AnimationStrings.Horizontal,relativeVector.x);
            _animator.SetFloat(AnimationStrings.Vertical,relativeVector.z);
        }
    }
}