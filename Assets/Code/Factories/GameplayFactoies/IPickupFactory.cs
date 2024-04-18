using Code.GameplayLogic.Weapons;
using Code.Pickups;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IPickupFactory : IFactory
    {
        Pickup CreateWeaponPickup(Vector3 position, WeaponType type);
    }
}