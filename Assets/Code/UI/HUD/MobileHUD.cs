using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
{
    public class MobileHUD : MonoBehaviour
    {
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _fireJoystick;
        [SerializeField] private Button _reloadButton;

        public Joystick MovementJoystick => _movementJoystick;
        public Joystick FireJoystick => _fireJoystick;
        public Button ReloadButton => _reloadButton;
    }
}