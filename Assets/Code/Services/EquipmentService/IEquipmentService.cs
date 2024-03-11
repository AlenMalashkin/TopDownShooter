using Code.GameplayLogic;

namespace Code.Services.EquipmentService
{
    public interface IEquipmentService : IService
    {
        WeaponType CurrentEquippedWeapon { get; }
        void EquipWeapon(WeaponType type);
    }
}