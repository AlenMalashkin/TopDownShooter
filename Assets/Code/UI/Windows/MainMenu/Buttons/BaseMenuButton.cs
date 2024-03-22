using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public abstract class BaseMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public virtual void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        public virtual void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}