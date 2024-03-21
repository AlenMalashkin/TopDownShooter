using System;
using UnityEngine.InputSystem;

namespace Code.Services.InputService.InputActions
{
    public class FireAction : IFireAction
    {
        private PlayerInputActions _playerInputActions;

        public FireAction(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }


        public bool FirePressed => _playerInputActions.Player.Fire.IsPressed();
    }
}