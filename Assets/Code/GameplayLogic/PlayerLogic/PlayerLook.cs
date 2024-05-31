using System;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using GamePush;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerLook : MonoBehaviour
    {
        private IInputService _inputService;
        private Joystick _movementJoystick;
        private Joystick _fireJoystick;
        private Camera _camera;
        private float _rayLength;

        public void Init(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
        }

        public void Init(Joystick movementJoystick, Joystick fireJoystick)
        {
            _movementJoystick = movementJoystick;
            _fireJoystick = fireJoystick;
        }

        private void Start()
        {
            if (!GP_Device.IsMobile())
                _inputService.GetInputAction<ILookAction>().SubscribeLookInput(LookAtMousePoint);
        }

        private void OnDisable()
        {
            if (!GP_Device.IsMobile())
                _inputService.GetInputAction<ILookAction>().UnsubscribeLookInput(LookAtMousePoint);
        }

        private void Update()
        {
            if (GP_Device.IsMobile())
                LookInJoystickDirection(_fireJoystick.Direction != Vector2.zero ? _fireJoystick : _movementJoystick);
        }

        private void LookAtMousePoint(InputAction.CallbackContext context)
        {
            Ray cameraRay = _camera.ScreenPointToRay(context.ReadValue<Vector2>());
            Plane planeRepresentation = new Plane(Vector3.up, transform.position);

            if (planeRepresentation.Raycast(cameraRay, out _rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(_rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        private void LookInJoystickDirection(Joystick joystick)
        {
            transform.eulerAngles = new Vector3(0f,
                Mathf.Atan2(joystick.Direction.x, joystick.Direction.y) * Mathf.Rad2Deg, 0f);
        }
    }
}