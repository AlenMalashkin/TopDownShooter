using Code.Infrastructure.GameStateMachine;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Level;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public class TestPlayButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;
        
        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void OnClick()
        {
            _gameStateMachine.Enter<GameState, LevelType>(LevelType.Main);
        }
    }
}