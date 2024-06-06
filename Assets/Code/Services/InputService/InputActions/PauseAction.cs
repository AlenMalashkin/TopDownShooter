using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class PauseAction : IPauseAction
    {
        private PlayerInputActions _playerInputActions;
        
        public PauseAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void SubscribePauseAction(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Pause.performed += action;

        public void UnsubscribePauseAction(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Pause.performed -= action;
    }
}