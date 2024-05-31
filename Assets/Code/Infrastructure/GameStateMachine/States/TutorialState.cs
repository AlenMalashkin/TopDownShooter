using Cinemachine;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Weapons;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Level;
using Code.Services;
using Code.Services.EnemiesProvider;
using Code.Services.EquipmentService;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.StaticData.TutorialStaticData;
using Code.UI.HUD;
using GamePush;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachine.States
{
    public class TutorialState : IGameState
    {
        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private IUIProvider _uiProvider;
        private IFactoryProvider _factoryProvider;
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;
        private IEnemiesProvider _enemiesProvider;

        private ServiceLocator _serviceLocator;
        private TutorialStaticData _tutorialStaticData;
        private ILevelFactory _levelFactory;
        private IPlayerFactory _playerFactory;
        private IWeaponFactory _weaponFactory;
        private IUIFactory _uiFactory;
        private IHUDFactory _hudFactory;
        private IEnemyFactory _enemyFactory;
        private Transform _uiRoot;
        private GameObject _player;
        private Weapon _playerWeapon;

        public TutorialState(ServiceLocator serviceLocator, IStaticDataService staticDataService,
            ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, IUIProvider uiProvider,
            IFactoryProvider factoryProvider, ISaveLoadService saveLoadService, IProgressService progressService,
            IEnemiesProvider enemiesProvider, IInputService inputService)
        {
            _serviceLocator = serviceLocator;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _uiProvider = uiProvider;
            _factoryProvider = factoryProvider;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _enemiesProvider = enemiesProvider;
        }

        public void Enter()
        {
            _levelFactory = _factoryProvider.GetFactory<ILevelFactory>();
            _playerFactory = _factoryProvider.GetFactory<IPlayerFactory>();
            _weaponFactory = _factoryProvider.GetFactory<IWeaponFactory>();
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _hudFactory = _factoryProvider.GetFactory<IHUDFactory>();
            _enemyFactory = _factoryProvider.GetFactory<IEnemyFactory>();

            _tutorialStaticData = _staticDataService.ForTutorial();
            _sceneLoadService.LoadScene("Main", OnLoad);
            _inputService.Enable();
            _enemiesProvider.EnemiesChanged += OnEnemiesCountChanged;
        }

        public void Exit()
        {
            _inputService.Disable();
            _enemiesProvider.Enemies.Clear();
            _enemiesProvider.EnemiesChanged -= OnEnemiesCountChanged;
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            _uiRoot = _uiFactory.CreateRoot().transform;
            _uiProvider.ChangeUIRoot(_uiRoot);

            InitializeLevel();

            InitializeHealthBar(_player.GetComponent<Damageable>());
            InitializeAmmoBar(_playerWeapon);

            _enemiesProvider.AddEnemy(
                _enemyFactory.CreateTutorialEnemy(_tutorialStaticData.TutorialLevel.EnemySpawnMarker.transform.position,
                    _player));
        }

        private void InitializeLevel()
        {
            _levelFactory.CreateTutorialLevel();
            
            _player = InitializePlayerAndCamera();
        }

        private GameObject InitializePlayerAndCamera()
        {
            _playerWeapon = _weaponFactory
                .CreateWeapon(_progressService.Progress.WeaponType);

            Camera mainCamera = Camera.main;

            GameObject player =
                _playerFactory.CreatePlayer(_tutorialStaticData.TutorialLevel.PlayerSpawnMarker.transform.position);
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
                UIJoysticks uiJoysticks = _uiFactory.CreateUIJoysticks(_uiRoot);

                playerShoot
                    .Init(uiJoysticks.FireJoystick, _playerWeapon);
                player.GetComponent<PlayerMovement>()
                    .Init(uiJoysticks.MovementJoystick);
                player.GetComponent<PlayerLook>()
                    .Init(uiJoysticks.MovementJoystick, uiJoysticks.FireJoystick);
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

        private void OnEnemiesCountChanged(int count)
        {
            if (count <= 0)
                _enemyFactory.CreateTutorialBoss(_tutorialStaticData.TutorialLevel.BossSpawnMarker.transform.position,
                    _uiRoot, _player);
        }
    }
}