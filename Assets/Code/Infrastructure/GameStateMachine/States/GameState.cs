using Cinemachine;
using Code.Factories.GameplayFactoies;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Spawners;
using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.Services;
using Code.Services.EquipmentService;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
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
        
        private LevelStaticData _levelStaticData;
        private ITimer _timer;
        private Spawner _spawner;

        public GameState(ISceneLoadService sceneLoadService, IStaticDataService staticDataService,
            LoadingScreen loadingScreen, IInputService inputService,
            IGameFactory gameFactory, IUpdater updater)
        {
            _sceneLoadService = sceneLoadService;
            _staticDataService = staticDataService;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _gameFactory = gameFactory;
            _updater = updater;
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
            
            _spawner = new EnemySpawner(_updater,
                ServiceLocator.Container.Resolve<IEnemyFactory>(), 
                ServiceLocator.Container.Resolve<IStaticDataService>());
            
            _spawner.EnableSpawner(player.transform);
        }

        private GameObject InitializePlayerAndCamera()
        {
            Weapon weapon = _gameFactory.CreatePlayerWeapon();

            Camera mainCamera = Camera.main;

            GameObject player = _gameFactory.CreatePlayer(_levelStaticData.PlayerPositionOnLevel);
            PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
            playerShoot
                .Init(ServiceLocator.Container.Resolve<IInputService>(), weapon);
            player.GetComponent<PlayerLook>()
                .Init(ServiceLocator.Container.Resolve<IInputService>(), mainCamera);
            player.GetComponent<PlayerMovement>()
                .Init(ServiceLocator.Container.Resolve<IInputService>());
            weapon.AttachToHand(playerShoot.PlayerArm);
            player.GetComponent<PlayerAnimator>()
                .Init(ServiceLocator.Container.Resolve<IEquipmentService>(), _inputService, mainCamera.transform);

            CinemachineVirtualCamera camera = _gameFactory.CreatePlayerCamera();
            camera.Follow = player.transform;

            return player;
        }
    }
}