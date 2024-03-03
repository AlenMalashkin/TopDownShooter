using System;
using System.Collections.Generic;
using Code.Infrastructure.GameStateMachine.States;

namespace Code.Infrastructure.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine()
        {
            //init states
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TPayloadState, TPayload>(TPayload payload) where TPayloadState : class, IPayloadState<TPayload>
        {
            TPayloadState state = ChangeState<TPayloadState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            
            TState newState = GetState<TState>();
            _currentState = newState;

            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitableState
            => _states[typeof(TState)] as TState;
    }
}