using System;
using System.Collections.Generic;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.TimerService;

namespace Code.Infrastructure.GameStateMachineNamespace
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _currentState;

        public GameStateMachine(ServiceLocator serviceLocator, ISceneLoadService sceneLoadService,
            LoadingScreen loadingScreen, ITimerService timerService)
        {
            _states[typeof(BootstrapState)] = new BootstrapState(serviceLocator, this, sceneLoadService, loadingScreen, timerService);
            _states[typeof(MenuState)] = new MenuState(sceneLoadService, loadingScreen, serviceLocator.Resolve<IUIFactory>());
            _states[typeof(GameState)] = new GameState(serviceLocator.Resolve<ISceneLoadService>(), serviceLocator.Resolve<IStaticDataService>(), loadingScreen, ServiceLocator.Container.Resolve<IInputService>(), serviceLocator.Resolve<IGameFactory>());
        }
        
        public void Enter<TState>() where TState : class, IGameState
        {
            IGameState gameState = ChangeState<TState>();
            gameState.Enter();
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