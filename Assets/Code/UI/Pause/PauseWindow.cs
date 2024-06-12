using System.Collections.Generic;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.PauseService;
using Code.UI.Windows;
using Code.UI.Windows.Buttons;
using TMPro;
using UnityEngine;

namespace Code.UI.Pause
{
    public class PauseWindow : BaseWindow, IClosableWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _pauseHeader;
        [SerializeField] private ResumeButton _resumeButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IPauseService pauseService, IGameStateMachine gameStateMachine)
        {
            _resumeButton.Init(pauseService, this);
            _backToMenuButton.Init(gameStateMachine, this);
        }

        public void Close()
            => Destroy(gameObject);

        public void Localize(Dictionary<string, string> localization)
        {
            _pauseHeader.text = localization["PauseHeader"];
            _resumeButton.SetButtonText(localization["ResumeButtonText"]);
            _backToMenuButton.SetButtonText(localization["BackToMenuButtonText"]);
        }
    }
}