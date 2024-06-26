using System.Collections.Generic;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ChooseLevelService;
using Code.UI.Windows.Buttons;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.WinWindow
{
    public class WinWindow : BaseWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private ToNextLevelButton _toNextLevelButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine, IChooseLevelService chooseLevelService)
        {
            _toNextLevelButton.Init(gameStateMachine, chooseLevelService);
            _backToMenuButton.Init(gameStateMachine);
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _winText.text = localization["WinText"];
            _toNextLevelButton.SetButtonText(localization["NextLevelButtonText"]);
            _backToMenuButton.SetButtonText(localization["ToMenuButtonText"]);
        }
    }
}