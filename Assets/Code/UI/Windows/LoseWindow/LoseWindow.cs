using System.Collections.Generic;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.UI.Windows.Buttons;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.LoseWindow
{
    public class LoseWindow : BaseWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _loseText;
        [SerializeField] private RetryLevelButton _retryLevelButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _retryLevelButton.Init(gameStateMachine);
            _backToMenuButton.Init(gameStateMachine);
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _loseText.text = localization["LoseText"];
            _retryLevelButton.SetButtonText(localization["RetryButtonText"]);
            _backToMenuButton.SetButtonText(localization["ToMenuButtonText"]);
        }
    }
}