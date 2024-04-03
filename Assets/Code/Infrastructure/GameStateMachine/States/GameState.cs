using Cinemachine;
using Code.Factories.GameplayFactoies;

using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Spawners;
using Code.GameplayLogic.Weapons;
using Code.GameplayLogic.Weapons.PlayerWeapons;
using Code.Level;
using Code.Services;
using Code.Services.EquipmentService;
using Code.Services.InputService;
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
        private IGameFactory _gameFactory;
        private IUpdater _updater;
        private IUIFactory _uiFactory;
        
        private LevelStaticData _levelStaticData;
        private ITimer _timer;
        private Spawner _spawner;

        private Weapon _playerWeapon;

        public GameState(ISceneLoadService sceneLoadService, IStaticDataService staticDataService,
            LoadingScreen loadingScreen, IInputService inputService,
            IGameFactory gameFactory, IUpdater updater, IUIFactory uiFactory)
        {
            _sceneLoadService = sceneLoadService;
            _staticDataService = staticDataService;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _gameFactory = gameFactory;
            _updater = updater;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
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
            
            InitializeHealthBar(player.GetComponent<Damageable>());
            InitializeAmmoBar(_playerWeapon);
            
            _spawner = new EnemySpawner(_updater,
                ServiceLocator.Container.Resolve<IEnemyFactory>(), 
                ServiceLocator.Container.Resolve<IStaticDataService>());
            
            _spawner.EnableSpawner(player.transform);
        }

        private GameObject InitializePlayerAndCamera()
        {
            _playerWeapon = _gameFactory.CreatePlayerWeapon();

            Camera mainCamera = Camera.main;

            GameObject player = _gameFactory.CreatePlayer(_levelStaticData.PlayerPositionOnLevel);
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

            CinemachineVirtualCamera camera = _gameFactory.CreatePlayerCamera();
            camera.Follow = player.transform;

            return player;
        }


        private void InitializeHealthBar(Damageable damageable)
        {
            _uiFactory.CreateRoot();
            HealthBar healthBar = _uiFactory.CreateProgressBar();
            
            healthBar.Init(damageable);
        }

        private void InitializeAmmoBar(Weapon playerWeapon)
        {
            _uiFactory.CreateRoot();
            AmmoBar ammoBar = _uiFactory.CreateAmmoBar();
            
            ammoBar.Init(playerWeapon);
        }


    }
}