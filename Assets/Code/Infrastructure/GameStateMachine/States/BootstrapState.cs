using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.EquipmentService;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.TimerService;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class BootstrapState : IGameState
    {
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _gameStateMachine;
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private ITimerService _timerService;
        private ITimer _timer;

        public BootstrapState(ServiceLocator serviceLocator, IGameStateMachine gameStateMachine,
            ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, ITimerService timerService)
        {
            _serviceLocator = serviceLocator;
            _gameStateMachine = gameStateMachine;
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _timerService = timerService;

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
            _gameStateMachine.Enter<MenuState>();
            _timerService.StartTimer(5, OnTimerFinishedOne);
            _timerService.StartTimer(6, OnTimerFinishedTwo);
            _timer = _timerService.StartTimer(7, OnTimerFinishedThree);
            _timerService.StartTimer(8);
            
            _timerService.StopTimer(_timer);
        }

        private void OnTimerFinishedOne()
        {
            Debug.Log("Tiemr 1 finished");
        }

        private void OnTimerFinishedTwo()
        {
            Debug.Log("Tiemr 2 finished");
        }

        private void OnTimerFinishedThree()
        {
            Debug.Log("Tiemr 3 finished");
        }

        private void RegisterAllServices()
        {
            _serviceLocator.RegisterService(_timerService);
            _serviceLocator.RegisterService(_gameStateMachine);
            _serviceLocator.RegisterService(_sceneLoadService);
            _serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            RegisterStaticDataService();
            _serviceLocator.RegisterService<IEquipmentService>(
                new EquipmentService(_serviceLocator.Resolve<IStaticDataService>()));
            _serviceLocator.RegisterService<IGameFactory>(new GameFactory(_serviceLocator.Resolve<IAssetProvider>(),
                _serviceLocator.Resolve<IStaticDataService>(), _serviceLocator.Resolve<IEquipmentService>()));
            _serviceLocator.RegisterService<IUIFactory>(new UIFactory(_serviceLocator.Resolve<IAssetProvider>(),
                _serviceLocator.Resolve<IStaticDataService>()));
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