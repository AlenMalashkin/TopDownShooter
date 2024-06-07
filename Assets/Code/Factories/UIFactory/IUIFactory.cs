using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.UI.EquipmentMenu;
using Code.UI.HUD;
using Code.UI.Windows.ChooseLevelWindow;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IUIFactory : IFactory
    {
        GameObject CreateRoot();
        EquipmentItem CreateEquipmentItem(WeaponType type, Transform root);
        LevelCard CreateLevelCard(LevelType type, Transform root);
        UIJoysticks CreateUIJoysticks(Transform root);
        PauseButton CreateUIPauseButton(Transform root);
    }
}
