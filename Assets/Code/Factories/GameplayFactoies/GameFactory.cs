using Cinemachine;
using Code.GameplayLogic;
using Code.Services.AssetProvider;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        
        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            GameObject playerPrefab = _assetProvider.LoadAsset(AssetPaths.Player);
            return Object.Instantiate(playerPrefab, position, Quaternion.identity);
        }

        public IWeapon CreateWeapon()
        {
            GameObject weaponPrefab = _assetProvider.LoadAsset(AssetPaths.PlayerWeapon);
            IWeapon weapon = Object.Instantiate(weaponPrefab).GetComponent<IWeapon>();
            return weapon;
        }

        public CinemachineVirtualCamera CreatePlayerCamera()
        {
            CinemachineVirtualCamera playerCameraPrefab =
                _assetProvider.LoadAsset<CinemachineVirtualCamera>(AssetPaths.PlayerCamera);
            
             return Object.Instantiate(playerCameraPrefab);
        }
    }
}