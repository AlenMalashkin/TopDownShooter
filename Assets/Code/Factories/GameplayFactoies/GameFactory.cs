using Cinemachine;
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

        public CinemachineVirtualCamera CreatePlayerCamera(Transform target)
        {
            CinemachineVirtualCamera playerCameraPrefab =
                _assetProvider.LoadAsset<CinemachineVirtualCamera>(AssetPaths.PlayerCamera);

            playerCameraPrefab.Follow = target;
            
             return Object.Instantiate(playerCameraPrefab);
        }
    }
}