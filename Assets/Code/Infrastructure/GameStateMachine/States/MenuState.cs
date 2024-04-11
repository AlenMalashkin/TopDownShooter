using Code.Factories.UIFactory;
using Code.Services.SceneLoadService;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachineNamespace.States
{
    public class MenuState : IGameState
    {
        private ISceneLoadService _sceneLoadService;
        private LoadingScreen _loadingScreen;
        private IFactoryProvider _factoryProvider;
        private IUIFactory _uiFactory;
        private IWindowFactory _windowFactory;
        
        public MenuState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen, IFactoryProvider factoryProvider)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _factoryProvider = factoryProvider;
        }
        
        public void Enter()
        {
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _windowFactory = _factoryProvider.GetFactory<IWindowFactory>();
            _sceneLoadService.LoadScene("Menu", OnLoad);
        }

        public void Exit()
        {
        }

        private void OnLoad()
        {
            GameObject root = _uiFactory.CreateRoot();
            _windowFactory.CreateMainMenu(root.transform);
            _loadingScreen.Hide();
        }
    }
}