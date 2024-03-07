using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface IFireAction : IInputAction
    {
        void SubscribeFireInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeFireInput(Action<InputAction.CallbackContext> action);
    }
}