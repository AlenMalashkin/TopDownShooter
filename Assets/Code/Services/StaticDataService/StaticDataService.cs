using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.Pickups;
using Code.StaticData.BossStaticData;
using Code.StaticData.EnemyStaticData;
using Code.StaticData.LevelStaticData;
using Code.StaticData.PickupStaticData;
using Code.StaticData.SpawnerStaticData;
using Code.StaticData.WeaponStaticData;
using Code.StaticData.WindowStaticData;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponType, WeaponData> _weaponsData = new Dictionary<WeaponType, WeaponData>();
        private Dictionary<LevelType, LevelStaticData> _levelsData = new Dictionary<LevelType, LevelStaticData>();
        private Dictionary<WindowType, WindowData> _windowsData = new Dictionary<WindowType, WindowData>();

        private Dictionary<LevelType, SpawnerStaticData> _spawnersStaticData =
            new Dictionary<LevelType, SpawnerStaticData>();

        private Dictionary<EnemyType, EnemyStaticData>
            _enemiesStaticData = new Dictionary<EnemyType, EnemyStaticData>();

        private Dictionary<BossType, BossStaticData> _bosses = new Dictionary<BossType, BossStaticData>();
        private Dictionary<WeaponType, WeaponPickup> _weaponPickups = new Dictionary<WeaponType, WeaponPickup>();

        public void Load()
        {
            _weaponsData = Resources.Load<WeaponStaticData>("StaticData/WeaponsConfig")
                .WeaponsData
                .ToDictionary(x => x.Type);

            _levelsData = Resources.LoadAll<LevelStaticData>("StaticData/LevelConfig")
                .ToDictionary(x => x.Type);

            _windowsData = Resources.Load<WindowStaticData>("StaticData/WindowConfig")
                .Windows
                .ToDictionary(x => x.Type);

            _spawnersStaticData = Resources.LoadAll<SpawnerStaticData>("StaticData/SpawnerStaticData")
                .ToDictionary(x => x.LevelType);

            _enemiesStaticData = Resources.LoadAll<EnemyStaticData>("StaticData/EnemyStaticData")
                .ToDictionary(x => x.Type);

            _bosses = Resources.LoadAll<BossStaticData>("StaticData/BossStaticData")
                .ToDictionary(x => x.Type);

            _weaponPickups = Resources.Load<PickupStaticData>("StaticData/PickupStaticData/PickupsConfig")
                .WeaponPickupData
                .ToDictionary(x => x.Type, x => x.Prefab);
        }

        public WeaponData ForWeapon(WeaponType type)
            => _weaponsData[type];

        public LevelStaticData ForLevel(LevelType type)
            => _levelsData[type];

        public WindowData ForWindow(WindowType type)
            => _windowsData[type];

        public SpawnerStaticData ForSpawner(LevelType type)
            => _spawnersStaticData[type];

        public EnemyStaticData ForEnemy(EnemyType type)
            => _enemiesStaticData[type];

        public BossStaticData ForBoss(BossType type)
            => _bosses[type];

        public WeaponPickup ForWeaponPickup(WeaponType type)
            => _weaponPickups[type];
    }
}