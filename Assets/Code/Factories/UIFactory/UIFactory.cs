using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services;
using Code.Services.AssetProvider;
using Code.Services.StaticDataService;
using Code.UI.HUD;
using Code.UI.Windows;
using Code.UI.Windows.MainMenu;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;
        
        private Transform _root;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void CreateRoot()
        {
            GameObject uiRoot = _assetProvider.LoadAsset("Prefabs/UIRoot");
            _root = Object.Instantiate(uiRoot).transform;
        }

        public MainMenuWindow CreateMainMenu()
        {
            BaseWindow window =_staticDataService.ForWindow(WindowType.MainMenu).WindowPrefab;
            MainMenuWindow menuWindow = Object.Instantiate(window, _root) as MainMenuWindow;
            menuWindow.TestPlayButton.Init(ServiceLocator.Container.Resolve<IGameStateMachine>());
            return menuWindow;
        }

        public HealthBar CreateProgressBar()
        {
            HealthBar healthBar = _assetProvider.LoadAsset<HealthBar>("Prefabs/HealthBar");

            return Object.Instantiate(healthBar, _root);
        }

        public AmmoBar CreateAmmoBar()
        {
            AmmoBar ammoBar = _assetProvider.LoadAsset<AmmoBar>("Prefabs/AmmoBar");

                return Object.Instantiate(ammoBar, _root);
        }
    }
}