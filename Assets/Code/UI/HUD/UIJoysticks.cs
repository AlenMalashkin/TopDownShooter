using UnityEngine;

namespace Code.UI.HUD
{
    public class UIJoysticks : MonoBehaviour
    {
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _fireJoystick;

        public Joystick MovementJoystick => _movementJoystick;
        public Joystick FireJoystick => _fireJoystick;
    }
}