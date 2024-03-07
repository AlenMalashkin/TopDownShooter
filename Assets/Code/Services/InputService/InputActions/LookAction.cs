using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class LookAction : ILookAction
    {
        private PlayerInputActions _playerInputActions;

        public LookAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void SubscribeLookInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Look.performed += action;

        public void UnsubscribeLookInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Look.performed -= action;
    }
}