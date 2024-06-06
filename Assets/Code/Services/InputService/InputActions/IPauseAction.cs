using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface IPauseAction : IInputAction
    {
        void SubscribePauseAction(Action<InputAction.CallbackContext> action);
        void UnsubscribePauseAction(Action<InputAction.CallbackContext> action);
    }
}