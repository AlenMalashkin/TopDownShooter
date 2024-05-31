using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Level;
using Code.UI.Windows.MainMenu.Buttons;

namespace Code.UI.Windows.Buttons
{
    public class RetryLevelButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void OnClick()
        {
            _gameStateMachine.Enter<GameState, LevelType>(LevelType.Level1);
        }
    }
}