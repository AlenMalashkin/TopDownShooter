using System;
using Code.GameplayLogic;
using Code.Pickups;

namespace Code.Data
{
    [Serializable]
    public class WeaponPickupData
    {
        public Pickup Prefab;
        public WeaponType Type;
    }
}