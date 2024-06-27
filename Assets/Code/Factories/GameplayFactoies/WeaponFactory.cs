using Code.Audio;
using Code.Data;
using Code.GameplayLogic.Weapons;
using Code.GameplayLogic.Weapons.PlayerWeapons;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Factories.GameplayFactoies
{
    public class WeaponFactory : IWeaponFactory
    {
        private IStaticDataService _staticDataService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public WeaponFactory(IStaticDataService staticDataService, IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public Weapon CreateWeapon(WeaponType type)
        {
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            PlayerWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<PlayerWeapon>();
            weapon.Init(this, weaponData.Bullet);
            weapon.GetComponent<SoundPlayer>().Init(_progressService, _saveLoadService);
            return weapon;
        }

        public Weapon CreateEnemyWeapon(EnemyWeaponType type)
        {
            EnemyWeaponData weaponData = _staticDataService.ForEnemyWeapon(type);
            PlayerWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<PlayerWeapon>();
            weapon.Init(this, weaponData.Bullet);
            weapon.GetComponent<SoundPlayer>().Init(_progressService, _saveLoadService);
            return weapon;
        }

        public Bullet CreateBullet(Vector3 spawnPosition, Bullet bulletPrefab)
        {
            return Object.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        }
    }
}