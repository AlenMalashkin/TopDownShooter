using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService
{
    public interface IInputService : IService
    {
        void Enable();
        void Disable();
        void SubscribeMovementInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeMovementInput(Action<InputAction.CallbackContext> action);
        void SubscribeFireInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeFireInput(Action<InputAction.CallbackContext> action);
        void SubscribeLookInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeLookInput(Action<InputAction.CallbackContext> action);
    }
}