using Code.Infrastructure.GameStateMachine.States;
using Code.Infrastructure.GameStateMachineNamespace;

namespace Code.Services.GameResultService
{
    public class GameFinishService : IGameFinishService
    {
        private IGameStateMachine _gameStateMachine;

        public GameFinishService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void FinishGameWithResult(GameResult gameResult)
            => _gameStateMachine.Enter<GameResultState, GameResult>(gameResult);
    }
}