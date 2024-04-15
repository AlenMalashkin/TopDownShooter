using Cinemachine;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Spawners;
using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.Services;
using Code.Services.EquipmentService;
using Code.Services.InputService;
using Code.Services.RandomService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
using Code.UI.HUD;
using Code.Utils.Timer;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class GameState : IGameState
    {
        private ISceneLoadService _sceneLoadService;
        private IStaticDataService _staticDataService;
        private LoadingScreen _loadingScreen;
        private IInputService _inputService;
        private IUpdater _updater;
        private IFactoryProvider _factoryProvider;
        private IPlayerFactory _playerFactory;
        private IWeaponFactory _weaponFactory;
        private IUIFactory _uiFactory;
        private IHUDFactory _hudFactory;

        private LevelStaticData _levelStaticData;
        private ITimer _timer;
        private Spawner _spawner;
        private Transform _hudRoot;

        private Weapon _playerWeapon;

        public GameState(ISceneLoadService sceneLoadService, IStaticDataService staticDataService,
            LoadingScreen loadingScreen, IInputService inputService,
            IUpdater updater, IFactoryProvider factoryProvider)
        {
            _sceneLoadService = sceneLoadService;
            _staticDataService = staticDataService;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _updater = updater;
            _factoryProvider = factoryProvider;
        }

        public void Enter()
        {
            _playerFactory = _factoryProvider.GetFactory<IPlayerFactory>();
            _weaponFactory = _factoryProvider.GetFactory<IWeaponFactory>();
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _hudFactory = _factoryProvider.GetFactory<IHUDFactory>();

            _levelStaticData = _staticDataService.ForLevel(LevelType.Main);
            _sceneLoadService.LoadScene(_levelStaticData.LevelName, OnLoad);
            _inputService.Enable();
        }

        public void Exit()
        {
            _inputService.Disable();
            _spawner.DisableSpawner();
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            GameObject player = InitializePlayerAndCamera();

            _hudRoot = _uiFactory.CreateRoot().transform;
            
            _spawner = new EnemySpawner(_updater, _hudRoot,
                ServiceLocator.Container.Resolve<IEnemyFactory>(),
                ServiceLocator.Container.Resolve<IStaticDataService>(),
                ServiceLocator.Container.Resolve<IRandomService>());
            
            InitializeHealthBar(player.GetComponent<Damageable>());
            InitializeAmmoBar(_playerWeapon);

            _spawner.EnableSpawner(player.transform);
        }

        private GameObject InitializePlayerAndCamera()
        {
            IEquipmentService equipmentService = ServiceLocator.Container.Resolve<IEquipmentService>();

            _playerWeapon = _weaponFactory
                .CreateWeapon(equipmentService.CurrentEquippedWeapon);

            Camera mainCamera = Camera.main;

            GameObject player = _playerFactory.CreatePlayer(_levelStaticData.PlayerPositionOnLevel);
            PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
            playerShoot
                .Init(ServiceLocator.Container.Resolve<IInputService>(), _playerWeapon);
            player.GetComponent<PlayerLook>()
                .Init(ServiceLocator.Container.Resolve<IInputService>(), mainCamera);
            player.GetComponent<PlayerMovement>()
                .Init(ServiceLocator.Container.Resolve<IInputService>());
            _playerWeapon.AttachToHand(playerShoot.PlayerArm);
            player.GetComponent<PlayerAnimator>()
                .Init(ServiceLocator.Container.Resolve<IEquipmentService>(), _inputService, mainCamera.transform);

            CinemachineVirtualCamera camera = _playerFactory.CreatePlayerCamera();

            camera.Follow = player.transform;

            return player;
        }

        private void InitializeHealthBar(Damageable damageable)
        {
            HealthBar healthBar = _hudFactory.CreateProgressBar(_hudRoot);
            healthBar.Init(damageable);
        }

        private void InitializeAmmoBar(Weapon playerWeapon)
        {
            AmmoBar ammoBar = _hudFactory.CreateAmmoBar(_hudRoot);
            ammoBar.Init(playerWeapon);
        }
    }
}