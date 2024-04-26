using Code.GameplayLogic.Weapons;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IUIFactory : IFactory
    {
        GameObject CreateRoot();
        GameObject CreateEquipmentItem(WeaponType type, Transform root);
    }
}
