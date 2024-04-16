using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.Pickups;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class PickupFactory : IPickupFactory
    {
        private IStaticDataService _staticDataService;

        public PickupFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public Pickup CreateWeaponPickup(Vector3 position, WeaponType type)
        {
            Pickup pickup = _staticDataService.ForWeaponPickup(type);
            return Object.Instantiate(pickup, position, Quaternion.identity);
        }
    }
}