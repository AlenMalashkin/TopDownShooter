using Code.Factories.UIFactory;
using Code.GameplayLogic.Weapons;
using Code.Pickups;
using Code.Services.GameResultService;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.Tutorial;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class PickupFactory : IPickupFactory
    {
        private IStaticDataService _staticDataService;
        private IGameFinishService _gameFinishService;
        private IWindowFactory _windowFactory;
        private IUIProvider _uiProvider;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public PickupFactory(IStaticDataService staticDataService, IGameFinishService gameFinishService,
            IWindowFactory windowFactory, IUIProvider uiProvider, IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _gameFinishService = gameFinishService;
            _windowFactory = windowFactory;
            _uiProvider = uiProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public Pickup CreateWeaponPickup(Vector3 position, WeaponType type)
        {
            Pickup pickupPrefab = _staticDataService.ForWeaponPickup(type);
            WeaponPickup pickup = Object.Instantiate(pickupPrefab, position, Quaternion.identity) as WeaponPickup;
            pickup.Init(_gameFinishService, _progressService, _saveLoadService);
            return pickup;
        }

        public Pickup CrateTutorialPickup(Vector3 position, WeaponType type)
        {
            Pickup pickupPrefab = _staticDataService.ForWeaponPickup(type);
            TutorialPickup pickup = Object.Instantiate(pickupPrefab, position, Quaternion.identity) as TutorialPickup;
            pickup.Init(_windowFactory, _uiProvider.GetRoot());
            return pickup;
        }
    }
}