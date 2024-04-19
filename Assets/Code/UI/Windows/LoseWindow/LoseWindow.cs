using Code.Infrastructure.GameStateMachineNamespace;
using Code.UI.Windows.Buttons;
using UnityEngine;

namespace Code.UI.Windows.LoseWindow
{
    public class LoseWindow : BaseWindow
    {
        [SerializeField] private RetryLevelButton _retryLevelButton;
        [SerializeField] private BackToMenuButton _backToMenuButton;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _retryLevelButton.Init(gameStateMachine);
            _backToMenuButton.Init(gameStateMachine);
        }
    }
}