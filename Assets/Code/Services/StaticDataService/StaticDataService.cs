using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.GameplayLogic;
using Code.Level;
using Code.StaticData.LevelStaticData;
using Code.StaticData.WeaponStaticData;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponType, WeaponData> _weaponsData = new Dictionary<WeaponType, WeaponData>();
        private Dictionary<LevelType, LevelStaticData> _levelsData = new Dictionary<LevelType, LevelStaticData>();
        
        public void Load()
        {
            _weaponsData = Resources.Load<WeaponStaticData>("StaticData/WeaponsConfig")
                .WeaponsData
                .ToDictionary(x => x.Type);

            _levelsData = Resources.LoadAll<LevelStaticData>("StaticData/LevelConfig")
                .ToDictionary(x => x.Type);
        }

        public WeaponData ForWeapon(WeaponType type)
            => _weaponsData[type];

        public LevelStaticData ForLevel(LevelType type)
            => _levelsData[type];
    }
}