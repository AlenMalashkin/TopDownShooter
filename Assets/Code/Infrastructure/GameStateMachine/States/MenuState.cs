using Code.Factories.UIFactory;
using Code.Services.SceneLoadService;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class MenuState : IGameState
    {
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private IUIFactory _uiFactory;
        
        public MenuState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, IUIFactory uiFactory)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            _sceneLoadService.LoadScene("Menu", OnLoad);
        }

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _uiFactory.CreateRoot();
            _uiFactory.CreateMainMenu();
            _loadingScreen.Hide();
        }
    }
}