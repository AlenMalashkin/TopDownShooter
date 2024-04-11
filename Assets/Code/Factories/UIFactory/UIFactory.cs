using Code.Services.AssetProvider;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private IAssetProvider _assetProvider;
        
        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateRoot()
        {
            GameObject uiRoot = _assetProvider.LoadAsset("Prefabs/UIRoot");
            return Object.Instantiate(uiRoot);
        }
    }
}