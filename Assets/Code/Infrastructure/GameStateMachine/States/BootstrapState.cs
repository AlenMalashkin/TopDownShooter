using Code.Factories.GameplayFactoies;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class BootstrapState : IGameState
    {
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _gameStateMachine;
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;

        public BootstrapState(ServiceLocator serviceLocator, IGameStateMachine gameStateMachine, ISceneLoadService sceneLoadService, LoadingScreen loadingScreen)
        {
            _serviceLocator = serviceLocator;
            _gameStateMachine = gameStateMachine;
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            
            RegisterAllServices();
        }

        public void Enter()
        {
            _loadingScreen.Show();
            _sceneLoadService.LoadScene("Bootstrap", OnLoad);
        }

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _gameStateMachine.Enter<GameState>();
        }

        private void RegisterAllServices()
        {
            _serviceLocator.RegisterService(_gameStateMachine);
            _serviceLocator.RegisterService(_sceneLoadService);
            _serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            RegisterStaticDataService();
            _serviceLocator.RegisterService<IGameFactory>(new GameFactory(_serviceLocator.Resolve<IAssetProvider>()));
            _serviceLocator.RegisterService<IInputService>(new DesktopInputService(new PlayerInputActions()));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();
            _serviceLocator.RegisterService(staticDataService);
        }
    }
}