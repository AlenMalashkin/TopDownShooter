using Cinemachine;
using Code.Factories.GameplayFactoies;
using Code.GameplayLogic;
using Code.Services;
using Code.Services.InputService;
using Code.Services.SceneLoadService;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class GameState : IGameState
    {
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private IGameFactory _gameFactory;
        
        public GameState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            _sceneLoadService.LoadScene("Main", OnLoad);
        }

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            InitializePlayerAndCamera();
        }

        private void InitializePlayerAndCamera()
        {
            IWeapon weapon = _gameFactory.CreateWeapon();
            
            GameObject player = _gameFactory.CreatePlayer(new Vector3(0, 0, 0));
            PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
            playerShoot.Init(weapon, ServiceLocator.Container.Resolve<IInputService>());
            weapon.AttachToHand(playerShoot.PlayerArm);

            CinemachineVirtualCamera camera = _gameFactory.CreatePlayerCamera();
            camera.Follow = player.transform;
        }
    }
}