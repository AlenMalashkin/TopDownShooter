using System;
using System.Collections.Generic;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachine.States;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.ChooseLevelService;
using Code.Services.EnemiesProvider;
using Code.Services.InputService;
using Code.Services.ProgressService;
using Code.Services.RandomService;
using Code.Services.SaveService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;

namespace Code.Infrastructure.GameStateMachineNamespace
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _currentState;

        public GameStateMachine(ServiceLocator serviceLocator, ISceneLoadService sceneLoadService,
            LoadingScreen loadingScreen, IUpdater updater)
        {
            _states[typeof(BootstrapState)] =
                new BootstrapState(serviceLocator, this, sceneLoadService, loadingScreen, updater);
            _states[typeof(MenuState)] =
                new MenuState(sceneLoadService, loadingScreen, serviceLocator.Resolve<IFactoryProvider>(),
                    serviceLocator.Resolve<IUIProvider>());
            _states[typeof(TutorialState)] = new TutorialState(serviceLocator,
                serviceLocator.Resolve<IStaticDataService>(), sceneLoadService, loadingScreen,
                serviceLocator.Resolve<IUIProvider>(), serviceLocator.Resolve<IFactoryProvider>(),
                serviceLocator.Resolve<ISaveLoadService>(), serviceLocator.Resolve<IProgressService>(),
                serviceLocator.Resolve<IEnemiesProvider>(), serviceLocator.Resolve<IInputService>());
            _states[typeof(GameState)] = new GameState(serviceLocator, this,
                serviceLocator.Resolve<ISceneLoadService>(),
                serviceLocator.Resolve<IStaticDataService>(),
                loadingScreen, serviceLocator.Resolve<IInputService>(), updater,
                serviceLocator.Resolve<IFactoryProvider>(), serviceLocator.Resolve<IUIProvider>(),
                serviceLocator.Resolve<IEnemiesProvider>(), serviceLocator.Resolve<IRandomService>(),
                serviceLocator.Resolve<IProgressService>(), serviceLocator.Resolve<ISaveLoadService>(),
                serviceLocator.Resolve<IChooseLevelService>());
            _states[typeof(GameResultState)] = new GameResultState(serviceLocator.Resolve<IFactoryProvider>(),
                serviceLocator.Resolve<IUIProvider>());
        }

        public void Enter<TState>() where TState : class, IGameState
        {
            IGameState gameState = ChangeState<TState>();
            gameState.Enter();
        }

        public void Enter<TPayloadState, TPayload>(TPayload payload)
            where TPayloadState : class, IPayloadState<TPayload>
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