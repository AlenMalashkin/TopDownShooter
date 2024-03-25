using Code.Data;
using Code.GameplayLogic;
using Code.Services.StaticDataService;

namespace Code.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        public WeaponType CurrentEquippedWeapon => _currentEquippedWeapon;
        public WeaponCategory CurrentWeaponCategory => _currentWeaponCategory;

        private WeaponType _currentEquippedWeapon = WeaponType.Rifle;
        private WeaponCategory _currentWeaponCategory = WeaponCategory.Rifle;

        private IStaticDataService _staticDataService;

        public EquipmentService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void EquipWeapon(WeaponType type)
        {
            _currentEquippedWeapon = type;
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            _currentWeaponCategory = weaponData.Category;
        }
    }
}