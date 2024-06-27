using Code.Factories;
using Code.Factories.UIFactory;
using Code.Services.AssetProvider;
using Code.Services.SceneLoadService;
using Code.Services.UIProvider;
using GamePush;
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
        private IAudioFactory _audioFactory;
        private IAssetProvider _assetProvider;

        public MenuState(ISceneLoadService sceneLoadService, LoadingScreen loadingScreen,
            IFactoryProvider factoryProvider, IUIProvider uiProvider, IAssetProvider assetProvider)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
            _factoryProvider = factoryProvider;
            _uiProvider = uiProvider;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _uiFactory = _factoryProvider.GetFactory<IUIFactory>();
            _windowFactory = _factoryProvider.GetFactory<IWindowFactory>();
            _audioFactory = _factoryProvider.GetFactory<IAudioFactory>();
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
            _audioFactory.CreateSoundPlayer()
                .PlayMusic(_assetProvider.LoadAsset<AudioClip>("ExternalContent/Sounds/MenuMusic"));
            _loadingScreen.Hide();
        }
    }
}