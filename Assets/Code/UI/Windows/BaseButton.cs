using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        
        public virtual void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        public virtual void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();

        public void SetButtonText(string text)
            => _buttonText.text = text;
    }
}