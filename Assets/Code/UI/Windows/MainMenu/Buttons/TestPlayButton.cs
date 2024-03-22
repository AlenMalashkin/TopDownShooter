using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;

namespace Code.UI.Windows.MainMenu.Buttons
{
    public class TestPlayButton : BaseMenuButton
    {
        private IGameStateMachine _gameStateMachine;
        
        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void OnClick()
        {
            _gameStateMachine.Enter<GameState>();
        }
    }
}