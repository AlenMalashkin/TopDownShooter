using Code.Factories.UIFactory;
using Code.Services.SceneLoadService;
using Code.Services.UIProvider;
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
        private IUIProvider _uiProvider;

        public MenuState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen,
            IFactoryProvider factoryProvider, IUIProvider uiProvider)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _factoryProvider = factoryProvider;
            _uiProvider = uiProvider;
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
            _uiProvider.ChangeUIRoot(root.transform);
            _windowFactory.CreateMainMenu(root.transform);
            _loadingScreen.Hide();
        }
    }
}