using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ChooseLevelService;
using Code.UI.Windows.Buttons;
using UnityEngine;

namespace Code.UI.Windows.WinWindow
{
    public class WinWindow : BaseWindow
    {
        [SerializeField] private ToNextLevelButton _toNextLevelButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine, IChooseLevelService chooseLevelService)
        {
            _toNextLevelButton.Init(gameStateMachine, chooseLevelService);
            _backToMenuButton.Init(gameStateMachine);
        }
    }
}