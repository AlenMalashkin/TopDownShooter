using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;

namespace Code.Infrastructure.GameStateMachineNamespace
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IGameState;
        void Enter<TPayloadState, TPayload>(TPayload payload) where TPayloadState : class, IPayloadState<TPayload>;
    }
}