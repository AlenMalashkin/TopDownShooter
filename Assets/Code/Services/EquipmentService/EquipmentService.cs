using System;
using Code.Data;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;

namespace Code.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        public event Action<WeaponType> WeaponEquipped;
        public WeaponType CurrentEquippedWeapon => _progressService.Progress.WeaponType;
        public WeaponCategory CurrentWeaponCategory => _staticDataService.ForWeapon(_progressService.Progress.WeaponType).Category;

        private IStaticDataService _staticDataService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public EquipmentService(IStaticDataService staticDataService, IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void EquipWeapon(WeaponType type)
        {
            _progressService.Progress.WeaponType = type;
            _saveLoadService.SaveProgress();
            WeaponEquipped?.Invoke(type);
        }
    }
}