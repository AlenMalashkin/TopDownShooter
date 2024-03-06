using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private PlayerInputActions _playerInputActions;
        
        public DesktopInputService(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public void Enable()
            => _playerInputActions.Enable();
        
        public void Disable()
            => _playerInputActions.Disable();

        public void SubscribeMovementInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Move.performed += action;

        public void UnsubscribeMovementInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Move.performed -= action;

        public void SubscribeFireInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Fire.performed += action;

        public void UnsubscribeFireInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Fire.performed -= action;

        public void SubscribeLookInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Look.performed += action;

        public void UnsubscribeLookInput(Action<InputAction.CallbackContext> action)
            => _playerInputActions.Player.Look.performed -= action;
    }
}