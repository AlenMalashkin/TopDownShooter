using Cinemachine;
using Code.Data;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.GameplayLogic.Weapons.PlayerWeapons;
using Code.Services.AssetProvider;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class PlayerFactory : IPlayerFactory
    {
        private IAssetProvider _assetProvider;

        public PlayerFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            GameObject playerPrefab = _assetProvider.LoadAsset(AssetPaths.Player);
            return Object.Instantiate(playerPrefab, position, Quaternion.identity);
        }

        public CinemachineVirtualCamera CreatePlayerCamera()
        {
            CinemachineVirtualCamera playerCameraPrefab =
                _assetProvider.LoadAsset<CinemachineVirtualCamera>(AssetPaths.PlayerCamera);
            
             return Object.Instantiate(playerCameraPrefab);
        }
    }
}