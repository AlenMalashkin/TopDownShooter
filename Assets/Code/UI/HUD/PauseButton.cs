using Code.Factories.UIFactory;
using Code.Services.PauseService;
using Code.UI.Windows.MainMenu.Buttons;
using UnityEngine;

namespace Code.UI.HUD
{
    public class PauseButton : BaseButton
    {
        private IPauseService _pauseService;
        private IWindowFactory _windowFactory;
        private Transform _root;

        public void Init(IPauseService pauseService, IWindowFactory windowFactory, Transform root)
        {
            _pauseService = pauseService;
            _windowFactory = windowFactory;
            _root = root;
        }
        
        protected override void OnClick()
        {
            _pauseService.Pause();
            _windowFactory.CreatePauseWindow(_root);
        }
    }
}