﻿using Cinemachine;
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
using Code.Services.EnemiesProvider;
using Code.Services.EquipmentService;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.RandomService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.StaticData.LevelStaticData;
using Code.UI.HUD;
using Code.Utils.Timer;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class GameState : IGameState
    {
        private IGameStateMachine _gameStateMachine;
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
        private IWindowFactory _windowFactory;
        private IUIProvider _uiProvider;
        private IEnemiesProvider _enemiesProvider;
        private IRandomService _randomService;

        private LevelStaticData _levelStaticData;
        private ITimer _timer;
        private EnemySpawner _spawner;
        private Spawner _bossSpawner;
        private Transform _uiRoot;
        private GameObject _player;

        private Weapon _playerWeapon;

        public GameState(IGameStateMachine gameStateMachine, ISceneLoadService sceneLoadService,
            IStaticDataService staticDataService,
            LoadingScreen loadingScreen, IInputService inputService,
            IUpdater updater, IFactoryProvider factoryProvider, IUIProvider uiProvider,
            IEnemiesProvider enemiesProvider, IRandomService randomService)
        {
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
        }

        public void Enter()
        {
            _playerFactory = _factoryProvider.GetFactory<IPlayerFactory>();
            _weaponFactory = _factoryProvider.GetFactory<IWeaponFactory>();
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _hudFactory = _factoryProvider.GetFactory<IHUDFactory>();
            _windowFactory = _factoryProvider.GetFactory<IWindowFactory>();

            _levelStaticData = _staticDataService.ForLevel(LevelType.Main);
            _sceneLoadService.LoadScene(_levelStaticData.LevelName, OnLoad);
            _inputService.Enable();

            _enemiesProvider.EnemiesChanged += OnEnemiesCountChanged;
        }

        public void Exit()
        {
            _enemiesProvider.Enemies.Clear();
            _inputService.Disable();
            _spawner.DisableSpawner();
            _bossSpawner?.DisableSpawner();

            _enemiesProvider.EnemiesChanged -= OnEnemiesCountChanged;
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            _player = InitializePlayerAndCamera();

            _uiRoot = _uiFactory.CreateRoot().transform;
            _uiProvider.ChangeUIRoot(_uiRoot);

            InitializeSpawners();

            InitializeHealthBar(_player.GetComponent<Damageable>());
            InitializeAmmoBar(_playerWeapon);

            _spawner.EnableSpawner(_player.transform);
        }

        private void InitializeSpawners()
        {
            _spawner = new EnemySpawner(_updater,
                _factoryProvider,
                _staticDataService,
                _randomService,
                _enemiesProvider);

            if (_levelStaticData.BossType != BossType.None)
            {
                _bossSpawner = new BossSpawner(_factoryProvider,
                    _staticDataService,
                    _uiProvider);
            }
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
            player.GetComponent<PlayerDeath>().Init(ServiceLocator.Container.Resolve<IGameFinishService>());

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
                _gameStateMachine.Enter<GameResultState, GameResult>(GameResult.Win);
                
            if (enemiesCount <= 0 && _spawner.EnemiesRemaining <= 0 && _levelStaticData.BossType != BossType.None)
                _bossSpawner.EnableSpawner(_player.transform);
        }
    }
}