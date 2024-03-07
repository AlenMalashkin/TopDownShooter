using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface ILookAction : IInputAction
    {
        void SubscribeLookInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeLookInput(Action<InputAction.CallbackContext> action);
    }
}