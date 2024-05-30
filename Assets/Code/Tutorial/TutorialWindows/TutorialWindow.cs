using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.UI.Windows;
using Code.UI.Windows.Buttons;
using UnityEngine;

namespace Code.Tutorial.TutorialWindows
{
    public class TutorialWindow : BaseWindow
    {
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _backToMenuButton.Init(gameStateMachine);
            progressService.Progress.TutorialPassed = true;
            saveLoadService.SaveProgress();
        }
    }
}