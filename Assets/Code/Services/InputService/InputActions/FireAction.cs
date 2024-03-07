using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class FireAction : IFireAction
    {
        private PlayerInputActions _playerInputActions;

        public FireAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void SubscribeFireInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Fire.performed += action;
        
        public void UnsubscribeFireInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Fire.performed -= action; 
    }
}