using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class AIStateMachine : AIStateMachineBase
    {
        [SerializeField] private AIState[] _states;
        [SerializeField] private AIState _startState;

        public Type CurrentStateType =>_currentState.GetType();
        
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

            _currentState.enabled = true;
            _currentState.EnterState();
        }

        public override void EnterState<T>()
            => ChangeState<T>();

        public override void UpdateState()
            => _currentState.UpdateState();

        private void ChangeState<T>() where T : AIState
        {
            if (typeof(T) == _currentState.GetType())
                return;

            _currentState.enabled = false;
            _currentState.ExitState();
            AIState state = GetState<T>();
            _currentState = state;
            _currentState.enabled = true;
            state.EnterState();
        }

        private AIState GetState<T>() where T : AIState
            => _statesMap[typeof(T)];
    }
}