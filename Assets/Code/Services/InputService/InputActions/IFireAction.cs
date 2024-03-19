using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public interface IFireAction : IInputAction
    {
        bool FirePressed { get; }
    }
}