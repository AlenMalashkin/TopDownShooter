using Code.Services.SceneLoadService;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class GameState : IGameState
    {
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        
        public GameState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
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
        }
    }
}