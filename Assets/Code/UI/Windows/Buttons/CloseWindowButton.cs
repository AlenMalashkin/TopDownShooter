using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Buttons
{
    public class CloseWindowButton : MonoBehaviour
    {
        [SerializeField] private BaseWindow _window;
        [SerializeField] private Button _button;

        private void OnEnable()
            => _button.onClick.AddListener(CloseWindow);

        private void OnDisable()
            => _button.onClick.RemoveListener(CloseWindow);

        private void CloseWindow()
            => Destroy(_window.gameObject);
    }
}