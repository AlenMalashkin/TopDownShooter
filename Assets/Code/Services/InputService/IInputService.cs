using System;
using Code.Services.InputService.InputActions;
using UnityEngine.InputSystem;

namespace Code.Services.InputService
{
    public interface IInputService : IService
    {
        void Enable();
        void Disable();
        T GetInputAction<T>() where T : IInputAction;
    }
}