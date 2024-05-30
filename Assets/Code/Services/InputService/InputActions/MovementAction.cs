using System;
using System.Numerics;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class MovementAction : IMovementAction
    {
        private PlayerInputActions _playerInputActions;

        public MovementAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void SubscribeMovementInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Move.performed += action;

        public void UnsubscribeMovementInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Move.performed -= action;
    }
}