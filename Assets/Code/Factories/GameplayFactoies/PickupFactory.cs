using Code.GameplayLogic.Weapons;
using Code.Pickups;
using Code.Services.GameResultService;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class PickupFactory : IPickupFactory
    {
        private IStaticDataService _staticDataService;
        private IGameFinishService _gameFinishService;
        
        public PickupFactory(IStaticDataService staticDataService, IGameFinishService gameFinishService)
        {
            _staticDataService = staticDataService;
            _gameFinishService = gameFinishService;
        }
        
        public Pickup CreateWeaponPickup(Vector3 position, WeaponType type)
        {
            Pickup pickupPrefab = _staticDataService.ForWeaponPickup(type);
            WeaponPickup pickup = Object.Instantiate(pickupPrefab, position, Quaternion.identity) as WeaponPickup;
            pickup.Init(_gameFinishService);
            return pickup;
        }
    }
}