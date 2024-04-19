using Code.Infrastructure.GameStateMachineNamespace;
using Code.UI.Windows.Buttons;
using UnityEngine;

namespace Code.UI.Windows.WinWindow
{
    public class WinWindow : BaseWindow
    {
        [SerializeField] private ToNextLevelButton _toNextLevelButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _toNextLevelButton.Init(gameStateMachine);
            _backToMenuButton.Init(gameStateMachine);
        }
    }
}