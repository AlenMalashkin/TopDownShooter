using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.PauseService;
using Code.UI.Windows;
using Code.UI.Windows.Buttons;
using UnityEngine;

namespace Code.UI.Pause
{
    public class PauseWindow : BaseWindow, IClosableWindow
    {
        [SerializeField] private ResumeButton _resumeButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IPauseService pauseService, IGameStateMachine gameStateMachine)
        {
            _resumeButton.Init(pauseService, this);
            _backToMenuButton.Init(gameStateMachine, this);
        }

        public void Close()
            => Destroy(gameObject);
    }
}