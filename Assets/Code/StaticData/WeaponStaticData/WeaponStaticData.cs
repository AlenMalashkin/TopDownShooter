using Code.Data;
using UnityEngine;

namespace Code.StaticData.WeaponStaticData
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Weapons Static Data", order = 0)]
    public class WeaponStaticData : ScriptableObject
    {
        [SerializeField] private WeaponData[] _weaponsData;
        [SerializeField] private EnemyWeaponData[] _enemyWeaponsData;
        public WeaponData[] WeaponsData => _weaponsData;
        public EnemyWeaponData[] EnemyWeaponsData => _enemyWeaponsData;
    }
}