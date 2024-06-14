using Code.Data.Progress;
using Code.Factories;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.ChooseLevelService;
using Code.Services.EnemiesProvider;
using Code.Services.EquipmentService;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.LocalizationService;
using Code.Services.PauseService;
using Code.Services.ProgressService;
using Code.Services.RandomService;
using Code.Services.SaveService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.Utils.Timer;
using GamePush;
using UnityEngine;
using Random = System.Random;

namespace Code.Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IGameState
    {
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _gameStateMachine;
        private ISceneLoadService _sceneLoadService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
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
            LoadProgress();
            _serviceLocator.RegisterService(_progressService);
            _serviceLocator.RegisterService(_saveLoadService);
            _serviceLocator.RegisterService<IRandomService>(new RandomService(new Random()));
            _serviceLocator.RegisterService(_updater);
            _serviceLocator.RegisterService(_gameStateMachine);
            _serviceLocator.RegisterService(_sceneLoadService);
            _serviceLocator.RegisterService<IGameFinishService>(new GameFinishService(_gameStateMachine));
            _serviceLocator.RegisterService<IEnemiesProvider>(new EnemiesProvider());
            _serviceLocator.RegisterService<IUIProvider>(new UIProvider());
            _serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterService<IChooseLevelService>(new ChooseLevelService());
            RegisterStaticDataService();
            _serviceLocator.RegisterService<ILocalizationService>(
                new LocalizationService(_serviceLocator.Resolve<IStaticDataService>()));
            _serviceLocator.RegisterService<IEquipmentService>(
                new EquipmentService(_serviceLocator.Resolve<IStaticDataService>(),
                    _serviceLocator.Resolve<IProgressService>(), _serviceLocator.Resolve<ISaveLoadService>()));
            RegisterInput();
            _serviceLocator.RegisterService<IPauseService>(new PauseService(_serviceLocator.Resolve<IInputService>()));
            RegisterUIFactories();
            RegisterGameFactories();
            RegisterFactoryProvider();
        }

        private void RegisterInput()
        {
            _serviceLocator.RegisterService<IInputService>(new DesktopInputService(new PlayerInputActions()));
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
            factoryProvider.AddFactory<ILevelFactory>(_serviceLocator.Resolve<ILevelFactory>());

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
                    _serviceLocator.Resolve<IGameFinishService>(), _serviceLocator.Resolve<IWindowFactory>(),
                    _serviceLocator.Resolve<IUIProvider>(), _serviceLocator.Resolve<IProgressService>(),
                    _serviceLocator.Resolve<ISaveLoadService>()));
            _serviceLocator.RegisterService<IEnemyFactory>(new EnemyFactory(
                _serviceLocator.Resolve<IStaticDataService>(), _serviceLocator.Resolve<IWeaponFactory>(),
                _serviceLocator.Resolve<IHUDFactory>(), _serviceLocator.Resolve<IPickupFactory>(),
                _serviceLocator.Resolve<IEnemiesProvider>(), _updater));
            _serviceLocator.RegisterService<ILevelFactory>(new LevelFactory(_serviceLocator.Resolve<IAssetProvider>(),
                _serviceLocator.Resolve<IStaticDataService>()));
        }

        private void RegisterUIFactories()
        {
            _serviceLocator.RegisterService<IUIFactory>(new UIFactory(_gameStateMachine,
                _serviceLocator.Resolve<IChooseLevelService>(),
                _serviceLocator.Resolve<IAssetProvider>(),
                _serviceLocator.Resolve<IStaticDataService>(), _serviceLocator.Resolve<IEquipmentService>(),
                _progressService));
            _serviceLocator.RegisterService<IHUDFactory>(new HUDFactory(_serviceLocator.Resolve<IAssetProvider>()));
            _serviceLocator.RegisterService<IWindowFactory>(
                new WindowFactory(_serviceLocator.Resolve<IStaticDataService>(),
                    _serviceLocator.Resolve<IGameStateMachine>(), _serviceLocator.Resolve<IUIFactory>(),
                    _serviceLocator.Resolve<IChooseLevelService>(), _progressService, _saveLoadService,
                    _serviceLocator.Resolve<IPauseService>(), _serviceLocator.Resolve<IEquipmentService>(),
                    _serviceLocator.Resolve<ILocalizationService>()));
        }

        private void LoadProgress()
        {
            _progressService = new ProgressService();
            _saveLoadService = new SaveLoadService(_progressService);
            _progressService.Progress = _saveLoadService.LoadProgress() ?? new Progress();
        }
    }
}