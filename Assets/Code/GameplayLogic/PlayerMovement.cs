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

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
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