using System.Collections.Generic;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.UI.Windows;
using Code.UI.Windows.Buttons;
using TMPro;
using UnityEngine;

namespace Code.Tutorial.TutorialWindows
{
    public class TutorialPassedWindow : BaseWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _backToMenuButton.Init(gameStateMachine);
            progressService.Progress.TutorialPassed = true;
            saveLoadService.SaveProgress();
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _backToMenuButton.SetButtonText(localization["ToMenuButtonText"]);
            _winText.text = localization["WinText"];
        }
    }
}