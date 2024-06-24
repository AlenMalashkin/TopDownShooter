using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class ReloadAction : IReloadAction
    {
        private PlayerInputActions _playerInputActions;
        
        public ReloadAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void SubscribeReloadAction(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Reload.performed += action;

        public void UnsubscribeReloadAction(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Reload.performed -= action;
    }
}