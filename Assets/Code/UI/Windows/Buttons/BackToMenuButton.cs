using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.UI.Windows.MainMenu.Buttons;

namespace Code.UI.Windows.Buttons
{
    public class BackToMenuButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;
        private IClosableWindow _closableWindow;

        public void Init(IGameStateMachine gameStateMachine, IClosableWindow closableWindow = null)
        {
            _gameStateMachine = gameStateMachine;
            _closableWindow = closableWindow;
        }
        
        protected override void OnClick()
        {
            _closableWindow?.Close();

            _gameStateMachine.Enter<MenuState>();
        }
    }
}