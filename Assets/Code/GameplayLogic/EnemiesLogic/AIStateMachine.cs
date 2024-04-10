using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class AIStateMachine : AIStateMachineBase
    {
        [SerializeField] private AIState[] _states;
        [SerializeField] private AIState _startState;

        private Dictionary<Type, AIState> _statesMap = new Dictionary<Type, AIState>();
        private AIState _currentState;

        private void Awake()
        {
            foreach (var state in _states)
            {
                _statesMap.Add(state.GetType(), state);
            }

            if (_currentState == null)
                _currentState = _startState;
        }

        public override void EnterState<T>()
            => ChangeState<T>();

        public override void UpdateState()
            => _currentState.UpdateState();

        private void ChangeState<T>() where T : AIState
        {
            if (typeof(T) == _currentState.GetType())
                return;

            _currentState.ExitState();
            AIState state = GetState<T>();
            _currentState = state;
            state.EnterState();
        }

        private AIState GetState<T>() where T : AIState
            => _statesMap[typeof(T)];
    }
}