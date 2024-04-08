using Cinemachine;
using Code.Data;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.GameplayLogic.Weapons.PlayerWeapons;
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

        public Weapon CreateWeapon(WeaponType type)
        {
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            PlayerWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<PlayerWeapon>();
            weapon.Init(this);
            return weapon;
        }

        public Bullet CreateBullet(Vector3 spawnPosition, int damage, Vector3 direction)
        {
            WeaponData weaponData = _staticDataService.ForWeapon(_equipmentService.CurrentEquippedWeapon);
            Bullet bullet = Object.Instantiate(weaponData.Bullet, spawnPosition, Quaternion.identity);
            bullet.Init(damage, direction);
            return bullet;
        }

        public CinemachineVirtualCamera CreatePlayerCamera()
        {
            CinemachineVirtualCamera playerCameraPrefab =
                _assetProvider.LoadAsset<CinemachineVirtualCamera>(AssetPaths.PlayerCamera);
            
             return Object.Instantiate(playerCameraPrefab);
        }
    }
}