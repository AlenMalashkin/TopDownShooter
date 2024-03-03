using Code.Infrastructure.GameStateMachine.States;

namespace Code.Infrastructure.GameStateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TPayloadState, TPayload>(TPayload payload) where TPayloadState : class, IPayloadState<TPayload>;
    }
}