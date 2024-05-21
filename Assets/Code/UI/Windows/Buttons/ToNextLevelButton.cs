using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Level;
using Code.Services;
using Code.Services.ChooseLevelService;
using Code.UI.Windows.MainMenu.Buttons;

namespace Code.UI.Windows.Buttons
{
    public class ToNextLevelButton : BaseButton
    {
        private IGameStateMachine _gameStateMachine;
        private IChooseLevelService _chooseLevelService;

        public void Init(IGameStateMachine gameStateMachine, IChooseLevelService chooseLevelService)
        {
            _gameStateMachine = gameStateMachine;
            _chooseLevelService = chooseLevelService;
        }

        protected override void OnClick()
        {
            _gameStateMachine.Enter<GameState, LevelType>(
                _chooseLevelService.ChooseLevel(_chooseLevelService.NextLevel));
        }
    }
}