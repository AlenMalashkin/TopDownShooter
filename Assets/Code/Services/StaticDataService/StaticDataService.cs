using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.GameplayLogic;
using Code.StaticData.WeaponStaticData;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponType, WeaponData> _weaponsData = new Dictionary<WeaponType, WeaponData>();
        
        public void Load()
        {
            _weaponsData = Resources.Load<WeaponStaticData>("StaticData/WeaponsConfig")
                .WeaponsData
                .ToDictionary(x => x.Type);
        }

        public WeaponData ForWeapon(WeaponType type)
            => _weaponsData[type];
    }
}