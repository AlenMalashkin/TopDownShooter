using Cinemachine;
using Code.Factories;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Spawners;
using Code.GameplayLogic.Weapons;
using Code.Infrastructure.GameStateMachine.States;
using Code.Level;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.ChooseLevelService;
using Code.Services.EnemiesProvider;
using Code.Services.EquipmentService;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.InputService.InputActions;
using Code.Services.PauseService;
using Code.Services.ProgressService;
using Code.Services.RandomService;
using Code.Services.SaveService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.StaticData.LevelStaticData;
using Code.UI.HUD;
using Code.Utils.Timer;
using GamePush;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class GameState : IPayloadState<LevelType>
    {
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _gameStateMachine;
        private ISceneLoadService _sceneLoadService;
        private IStaticDataService _staticDataService;
        private LoadingScreen _loadingScreen;
        private IInputService _inputService;
        private IUpdater _updater;
        private IFactoryProvider _factoryProvider;
        private IPlayerFactory _playerFactory;
        private IWeaponFactory _weaponFactory;
        private IWindowFactory _windowFactory;
        private IUIFactory _uiFactory;
        private IHUDFactory _hudFactory;
        private ILevelFactory _levelFactory;
        private IUIProvider _uiProvider;
        private IEnemiesProvider _enemiesProvider;
        private IRandomService _randomService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IChooseLevelService _chooseLevelService;
        private IPauseService _pauseService;
        private IAssetProvider _assetProvider;
        private IAudioFactory _audioFactory;

        private LevelStaticData _levelStaticData;
        private ITimer _timer;
        private EnemySpawner _spawner;
        private Spawner _bossSpawner;
        private Transform _uiRoot;
        private GameObject _player;

        private Weapon _playerWeapon;

        public GameState(ServiceLocator serviceLocator, IGameStateMachine gameStateMachine,
            ISceneLoadService sceneLoadService,
            IStaticDataService staticDataService,
            LoadingScreen loadingScreen, IInputService inputService,
            IUpdater updater, IFactoryProvider factoryProvider, IUIProvider uiProvider,
            IEnemiesProvider enemiesProvider, IRandomService randomService, IProgressService progressService,
            ISaveLoadService saveLoadService, IChooseLevelService chooseLevelService, IPauseService pauseService,
            IAssetProvider assetProvider)
        {
            _serviceLocator = serviceLocator;
            _gameStateMachine = gameStateMachine;
            _sceneLoadService = sceneLoadService;
            _staticDataService = staticDataService;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _updater = updater;
            _factoryProvider = factoryProvider;
            _uiProvider = uiProvider;
            _enemiesProvider = enemiesProvider;
            _randomService = randomService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _chooseLevelService = chooseLevelService;
            _pauseService = pauseService;
            _assetProvider = assetProvider;
        }

        public void Enter(LevelType payload)
        {
            _playerFactory = _factoryProvider.GetFactory<IPlayerFactory>();
            _weaponFactory = _factoryProvider.GetFactory<IWeaponFactory>();
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _hudFactory = _factoryProvider.GetFactory<IHUDFactory>();
            _levelFactory = _factoryProvider.GetFactory<ILevelFactory>();
            _windowFactory = _factoryProvider.GetFactory<IWindowFactory>();
            _audioFactory = _factoryProvider.GetFactory<IAudioFactory>();

            _levelStaticData = _staticDataService.ForLevel(payload);
            _sceneLoadService.LoadScene("Main", OnLoad);
            _inputService.Enable();

            _enemiesProvider.EnemiesChanged += OnEnemiesCountChanged;
            _inputService.GetInputAction<IPauseAction>().SubscribePauseAction(OnPausePressed);
        }

        public void Exit()
        {
            _enemiesProvider.Enemies.Clear();
            _inputService.Disable();
            _spawner.DisableSpawner();
            _bossSpawner?.DisableSpawner();
            _pauseService.Resume();

            _enemiesProvider.EnemiesChanged -= OnEnemiesCountChanged;
            _inputService.GetInputAction<IPauseAction>().UnsubscribePauseAction(OnPausePressed);

            foreach (var playerComponent in _player.GetComponentsInChildren<MonoBehaviour>())
            {
                playerComponent.enabled = false;
            }
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            _uiRoot = _uiFactory.CreateRoot().transform;
            _uiProvider.ChangeUIRoot(_uiRoot);
            
            _audioFactory.CreateSoundPlayer()
                .PlayLoop(_assetProvider.LoadAsset<AudioClip>("ExternalContent/Sounds/ActionMusic"));

            InitializeLevel();

            InitializeHealthBar(_player.GetComponent<Damageable>());
            InitializeAmmoBar(_playerWeapon);

            if (GP_Device.IsMobile())
                _uiFactory.CreateUIPauseButton(_uiRoot)
                    .Init(_pauseService, _windowFactory, _uiRoot);

            _spawner.EnableSpawner(_player.transform);
        }

        private void InitializeLevel()
        {
            _levelFactory.CreateLevel(_levelStaticData.Type);

            _player = InitializePlayerAndCamera();

            _spawner = new EnemySpawner(_updater,
                _factoryProvider,
                _staticDataService,
                _randomService,
                _enemiesProvider,
                _chooseLevelService);

            if (_levelStaticData.BossType != BossType.None)
            {
                _bossSpawner = new BossSpawner(_factoryProvider,
                    _staticDataService,
                    _uiProvider, _chooseLevelService);
            }
        }

        private GameObject InitializePlayerAndCamera()
        {
            _playerWeapon = _weaponFactory
                .CreateWeapon(_progressService.Progress.WeaponType);

            Camera mainCamera = Camera.main;

            GameObject player = _playerFactory.CreatePlayer(_levelStaticData.PlayerPositionOnLevel);
            player.GetComponent<Player>().Init(_serviceLocator.Resolve<IWindowFactory>(),
                _serviceLocator.Resolve<IUIProvider>(), _pauseService);
            PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
            playerShoot
                .Init(_serviceLocator.Resolve<IInputService>(), _playerWeapon);
            player.GetComponent<PlayerLook>()
                .Init(_serviceLocator.Resolve<IInputService>(), mainCamera);
            player.GetComponent<PlayerMovement>()
                .Init(_serviceLocator.Resolve<IInputService>());
            _playerWeapon.AttachToHand(playerShoot.PlayerArm);
            player.GetComponent<PlayerAnimator>()
                .Init(_serviceLocator.Resolve<IEquipmentService>(), mainCamera.transform);
            player.GetComponent<PlayerDeath>().Init(_serviceLocator.Resolve<IGameFinishService>());

            if (GP_Device.IsMobile())
            {
                MobileHUD mobileHUD = _uiFactory.CreateMobileHUD(_uiRoot);

                playerShoot
                    .Init(mobileHUD.FireJoystick, _playerWeapon, mobileHUD.ReloadButton);
                player.GetComponent<PlayerMovement>()
                    .Init(mobileHUD.MovementJoystick);
                player.GetComponent<PlayerLook>()
                    .Init(mobileHUD.MovementJoystick, mobileHUD.FireJoystick);
                _playerWeapon.AttachToHand(playerShoot.PlayerArm);
            }

            CinemachineVirtualCamera camera = _playerFactory.CreatePlayerCamera();

            camera.Follow = player.transform;

            return player;
        }

        private void InitializeHealthBar(Damageable damageable)
        {
            HealthBar healthBar = _hudFactory.CreateProgressBar(_uiRoot);
            healthBar.Init(damageable);
        }

        private void InitializeAmmoBar(Weapon playerWeapon)
        {
            AmmoBar ammoBar = _hudFactory.CreateAmmoBar(_uiRoot);
            ammoBar.Init(playerWeapon);
        }

        private void OnEnemiesCountChanged(int enemiesCount)
        {
            if (enemiesCount <= 0 && _spawner.EnemiesRemaining <= 0 && _levelStaticData.BossType == BossType.None)
            {
                _gameStateMachine.Enter<GameResultState, GameResult>(GameResult.Win);
            }

            if (enemiesCount <= 0 && _spawner.EnemiesRemaining <= 0 && _levelStaticData.BossType != BossType.None)
                _bossSpawner.EnableSpawner(_player.transform);
        }

        private void OnPausePressed(InputAction.CallbackContext ctx)
        {
            _windowFactory.CreatePauseWindow(_uiRoot);
            _pauseService.Pause();
        }
    }
}