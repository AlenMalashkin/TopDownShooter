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
        private bool _isMoving;
        

        public float CurrentMoveSpeed
        {
            get
            {
                if (_isMoving)
                {
                    return _speed;
                }

                return 0;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector3>();
            _isMoving = _moveDirection != Vector3.zero;
        }

        private void Update()
        {
            _controller.Move(_moveDirection * CurrentMoveSpeed * Time.deltaTime);
        }

        private void OnValidate()
        {
            _controller ??= GetComponent<CharacterController>();
        }
    }
}