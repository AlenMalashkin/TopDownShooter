using System;
using System.Collections.Generic;
using Code.Services.InputService.InputActions;

namespace Code.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private PlayerInputActions _playerInputActions;
        private Dictionary<Type, IInputAction> _inputActions;
        
        public DesktopInputService(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;

            _inputActions = new Dictionary<Type, IInputAction>
            {
                [typeof(IMovementAction)] = new MovementAction(_playerInputActions),
                [typeof(IFireAction)] = new FireAction(_playerInputActions),
                [typeof(ILookAction)] = new LookAction(_playerInputActions),
                [typeof(IPauseAction)] = new PauseAction(_playerInputActions)
            };
        }

        public void Enable()
            => _playerInputActions.Enable();
        
        public void Disable()
            => _playerInputActions.Disable();
        
        public T GetInputAction<T>() where T : IInputAction
            => (T) _inputActions[typeof(T)];
    }
}