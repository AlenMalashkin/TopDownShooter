using Code.GameplayLogic;

namespace Code.Services.EquipmentService
{
    public interface IEquipmentService : IService
    {
        WeaponType CurrentEquippedWeapon { get; }
        WeaponCategory CurrentWeaponCategory { get; }
        void EquipWeapon(WeaponType type);
    }
}