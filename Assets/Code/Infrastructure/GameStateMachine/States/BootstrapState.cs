using Code.Factories;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.EnemiesProvider;
using Code.Services.EquipmentService;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.RandomService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.Utils.Timer;
using UnityEngine;
using Random = System.Random;

namespace Code.Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IGameState
    {
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _gameStateMachine;
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private IUpdater _updater;
        private ITimer _timer;

        public BootstrapState(ServiceLocator serviceLocator, IGameStateMachine gameStateMachine,
            ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, IUpdater updater)
        {
            _serviceLocator = serviceLocator;
            _gameStateMachine = gameStateMachine;
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _updater = updater;

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
        }

        private void RegisterAllServices()
        {
            _serviceLocator.RegisterService<IRandomService>(new RandomService(new Random()));
            _serviceLocator.RegisterService(_updater);
            _serviceLocator.RegisterService(_gameStateMachine);
            _serviceLocator.RegisterService(_sceneLoadService);
            _serviceLocator.RegisterService<IGameFinishService>(new GameFinishService(_gameStateMachine));
            _serviceLocator.RegisterService<IEnemiesProvider>(new EnemiesProvider());
            _serviceLocator.RegisterService<IUIProvider>(new UIProvider());
            _serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            RegisterStaticDataService();
            _serviceLocator.RegisterService<IEquipmentService>(
                new EquipmentService(_serviceLocator.Resolve<IStaticDataService>()));
            _serviceLocator.RegisterService<IInputService>(new DesktopInputService(new PlayerInputActions()));
            RegisterUIFactories();
            RegisterGameFactories();
            RegisterFactoryProvider();
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();
            _serviceLocator.RegisterService(staticDataService);
        }

        private void RegisterFactoryProvider()
        {
            IFactoryProvider factoryProvider = new FactoryProvider();

            factoryProvider.AddFactory<IUIFactory>(_serviceLocator.Resolve<IUIFactory>());
            factoryProvider.AddFactory<IHUDFactory>(_serviceLocator.Resolve<IHUDFactory>());
            factoryProvider.AddFactory<IWindowFactory>(_serviceLocator.Resolve<IWindowFactory>());

            factoryProvider.AddFactory<IPlayerFactory>(_serviceLocator.Resolve<IPlayerFactory>());
            factoryProvider.AddFactory<IWeaponFactory>(_serviceLocator.Resolve<IWeaponFactory>());
            factoryProvider.AddFactory<IEnemyFactory>(_serviceLocator.Resolve<IEnemyFactory>());
            factoryProvider.AddFactory<IPickupFactory>(_serviceLocator.Resolve<IPickupFactory>());

            _serviceLocator.RegisterService(factoryProvider);
        }

        private void RegisterGameFactories()
        {
            _serviceLocator.RegisterService<IPlayerFactory>(
                new PlayerFactory(_serviceLocator.Resolve<IAssetProvider>()));
            _serviceLocator.RegisterService<IWeaponFactory>(
                new WeaponFactory(_serviceLocator.Resolve<IStaticDataService>()));
            _serviceLocator.RegisterService<IPickupFactory>(
                new PickupFactory(_serviceLocator.Resolve<IStaticDataService>(),
                    _serviceLocator.Resolve<IGameFinishService>()));
            _serviceLocator.RegisterService<IEnemyFactory>(new EnemyFactory(
                _serviceLocator.Resolve<IStaticDataService>(), _serviceLocator.Resolve<IWeaponFactory>(),
                _serviceLocator.Resolve<IHUDFactory>(), _serviceLocator.Resolve<IPickupFactory>(),
                _serviceLocator.Resolve<IEnemiesProvider>()));
        }

        private void RegisterUIFactories()
        {
            _serviceLocator.RegisterService<IUIFactory>(new UIFactory(_serviceLocator.Resolve<IAssetProvider>(),
                _serviceLocator.Resolve<IStaticDataService>(), _serviceLocator.Resolve<IEquipmentService>()));
            _serviceLocator.RegisterService<IHUDFactory>(new HUDFactory(_serviceLocator.Resolve<IAssetProvider>()));
            _serviceLocator.RegisterService<IWindowFactory>(
                new WindowFactory(_serviceLocator.Resolve<IStaticDataService>(),
                    _serviceLocator.Resolve<IGameStateMachine>(), _serviceLocator.Resolve<IUIFactory>()));
        }
    }
}