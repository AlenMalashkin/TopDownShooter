using Code.Level;
using Code.Services.AssetProvider;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class LevelFactory : ILevelFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;

        public LevelFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        
        public GameObject CreateLevel(LevelType type)
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(type);
            return Object.Instantiate(_assetProvider.LoadAsset(levelStaticData.LevelPrefabPath));
        }
    }
}