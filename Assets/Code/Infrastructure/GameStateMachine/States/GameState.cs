using Cinemachine;
using Code.Factories.GameplayFactoies;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.RangeEnemy;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.Services;
using Code.Services.EquipmentService;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
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

        private LevelStaticData _levelStaticData;

        public GameState(ISceneLoadService sceneLoadService, IStaticDataService staticDataService, LoadingScreen loadingScreen, IInputService inputService,
            IGameFactory gameFactory)
        {
            _sceneLoadService = sceneLoadService;
            _staticDataService = staticDataService;
            _loadingScreen = loadingScreen;
            _inputService = inputService;
            _gameFactory = gameFactory;
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
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            GameObject player = InitializePlayerAndCamera();
            // InitializeEnemy(player.transform);
            InitializeRangeEnemy(player.transform);
        }

        private GameObject InitializePlayerAndCamera()
        {
            Weapon weapon = _gameFactory.CreateWeapon() as Weapon;
            weapon.Init(ServiceLocator.Container.Resolve<IGameFactory>());

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

        private void InitializeEnemy(Transform playerTransform)
        {
            GameObject enemy = _gameFactory.CreateEnemy(_levelStaticData
                .EnemySpanwers[Random.Range(0, _levelStaticData.EnemySpanwers.Count)]);
            enemy.GetComponent<EnemyMovement>()
                .Init(playerTransform);
        }

        private void InitializeRangeEnemy(Transform playerTransform)
        {
            GameObject rangeEnemy = _gameFactory.CreateRangeEnemy(_levelStaticData
                .EnemySpanwers[Random.Range(0, _levelStaticData.EnemySpanwers.Count)]);
            rangeEnemy.GetComponent<RangeEnemyMovement>().Init(playerTransform);
        }
    }
}