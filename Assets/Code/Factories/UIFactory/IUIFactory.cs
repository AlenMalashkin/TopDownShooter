using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.UI.HUD;
using Code.UI.Windows.ChooseLevelWindow;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IUIFactory : IFactory
    {
        GameObject CreateRoot();
        GameObject CreateEquipmentItem(WeaponType type, Transform root);
        LevelCard CreateLevelCard(LevelType type, Transform root);
        UIJoysticks CreateUIJoysticks(Transform root);
    }
}
