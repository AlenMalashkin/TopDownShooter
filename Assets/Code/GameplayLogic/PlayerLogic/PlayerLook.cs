using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerLook : MonoBehaviour
    {
        private IInputService _inputService;
        private Camera _camera;
        private float _rayLength;

        public void Init(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
        }

        private void Start()
        {
            _inputService.GetInputAction<ILookAction>().SubscribeLookInput(LookAtMousePoint);
        }

        private void OnDisable()
        {
            _inputService.GetInputAction<ILookAction>().UnsubscribeLookInput(LookAtMousePoint);
        }

        private void LookAtMousePoint(InputAction.CallbackContext context)
        {
            Ray cameraRay = _camera.ScreenPointToRay(context.ReadValue<Vector2>());
            Plane planeRepresentation = new Plane(Vector3.up, transform.position);
 
            if(planeRepresentation.Raycast(cameraRay, out _rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(_rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
}