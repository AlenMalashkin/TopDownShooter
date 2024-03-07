using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface IMovementAction : IInputAction
    {
        void SubscribeMovementInput(Action<InputAction.CallbackContext> action);
        void UnsubscribeMovementInput(Action<InputAction.CallbackContext> action);
    }
}