using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface IReloadAction : IInputAction
    {
        void SubscribeReloadAction(Action<InputAction.CallbackContext> action);
        void UnsubscribeReloadAction(Action<InputAction.CallbackContext> action);
    }
}