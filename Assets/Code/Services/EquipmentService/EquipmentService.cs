using Code.GameplayLogic;

namespace Code.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        public WeaponType CurrentEquippedWeapon => _currentEquippedWeapon;

        private WeaponType _currentEquippedWeapon = WeaponType.Pistol;
        
        public void EquipWeapon(WeaponType type)
        {
            _currentEquippedWeapon = type;
        }
    }
}