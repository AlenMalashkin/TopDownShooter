using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour

    {
        [SerializeField] private float _speed;

        private Vector2 _moveDirection;
        private CharacterController _controller;

        private void Update()
        {
            _controller.Move(new Vector3(_moveDirection.x, 0, _moveDirection.y) * _speed * Time.deltaTime);
        }

        private void OnValidate()
        {
            _controller ??= GetComponent<CharacterController>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
    }
}