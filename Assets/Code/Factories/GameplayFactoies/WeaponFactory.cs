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
            weapon.Init(this, type);
            return weapon;
        }

        public Bullet CreateBullet(Vector3 spawnPosition, int damage, Vector3 direction)
        {
            WeaponData weaponData = _staticDataService.ForWeapon(WeaponType.Pistol);
            Bullet bullet = Object.Instantiate(weaponData.Bullet, spawnPosition, Quaternion.identity);
            bullet.Init(damage, direction);
            return bullet;
        }
    }
}