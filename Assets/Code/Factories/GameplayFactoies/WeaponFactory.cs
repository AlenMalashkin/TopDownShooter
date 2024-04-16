using Code.Data;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.GameplayLogic.Weapons.PlayerWeapons;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class WeaponFactory : IWeaponFactory
    {
        private IStaticDataService _staticDataService;

        public WeaponFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public Weapon CreateWeapon(WeaponType type)
        {
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            PlayerWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<PlayerWeapon>();
            weapon.Init(this, weaponData.Bullet);
            return weapon;
        }

        public Bullet CreateBullet(Vector3 spawnPosition, Bullet bulletPrefab)
        {
            return Object.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        }
    }
}