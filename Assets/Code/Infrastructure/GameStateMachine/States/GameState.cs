using Code.Factories.GameplayFactoies;
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
            Debug.Log("game state");
        }

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _loadingScreen.Hide();

            Debug.Log("OnLoad");
            GameObject player = _gameFactory.CreatePlayer(new Vector3(0, 1, 0));
            _gameFactory.CreatePlayerCamera(player.transform);
        }
    }
}