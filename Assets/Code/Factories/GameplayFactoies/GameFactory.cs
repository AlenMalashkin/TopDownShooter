using Cinemachine;
using Code.Data;
using Code.GameplayLogic;
using Code.Services.AssetProvider;
using Code.Services.EquipmentService;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        private IEquipmentService _equipmentService;
        
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IEquipmentService equipmentService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _equipmentService = equipmentService;
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            GameObject playerPrefab = _assetProvider.LoadAsset(AssetPaths.Player);
            return Object.Instantiate(playerPrefab, position, Quaternion.identity);
        }
        

        public GameObject CreateEnemy(Vector3 position)
        {
            GameObject enemyPrefab = _assetProvider.LoadAsset(AssetPaths.Enemy);
            return Object.Instantiate(enemyPrefab, position, Quaternion.identity);
        }

        public IWeapon CreateWeapon()
        {
            WeaponData weaponData = _staticDataService.ForWeapon(_equipmentService.CurrentEquippedWeapon);
            IWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<IWeapon>();
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