using Code.Factories.UIFactory;
using Code.Services.UIProvider;
using UnityEngine;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public class SettingsButton : BaseButton
    {
        private IWindowFactory _windowFactory;
        private Transform _root;
        
        public void Init(IWindowFactory windowFactory, Transform root)
        {
            _windowFactory = windowFactory;
            _root = root;
        }
        
        protected override void OnClick()
        {
            _windowFactory.CreateSettingsWindow(_root);
        }
    }
}