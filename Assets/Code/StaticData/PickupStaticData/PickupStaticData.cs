using Code.Data;
using UnityEngine;

namespace Code.StaticData.PickupStaticData
{
    [CreateAssetMenu(fileName = "PickupStaticData", menuName = "Pickups", order = 5)]
    public class PickupStaticData : ScriptableObject
    {
        [SerializeField] private WeaponPickupData[] _weaponPickupData;
        public WeaponPickupData[] WeaponPickupData => _weaponPickupData;
    }
}