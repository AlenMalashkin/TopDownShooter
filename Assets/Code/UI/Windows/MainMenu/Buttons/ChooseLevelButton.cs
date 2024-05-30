using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachine.States;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ProgressService;
using UnityEngine;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public class ChooseLevelButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;
        private IWindowFactory _windowFactory;
        private IProgressService _progressService;
        private Transform _root;
        
        public void Init(IGameStateMachine gameStateMachine, IWindowFactory windowFactory, IProgressService progressService, Transform root)
        {
            _gameStateMachine = gameStateMachine;
            _windowFactory = windowFactory;
            _progressService = progressService;
            _root = root;
        }
        
        protected override void OnClick()
        {
            if (_progressService.Progress.TutorialPassed)
                _windowFactory.CreateChooseLevelWindow(_root);
            else
                _gameStateMachine.Enter<TutorialState>();
        }
    }
}