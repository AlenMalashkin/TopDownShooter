using System;
using Code.Factories.UIFactory;
using Code.GameplayLogic.Weapons;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.EquipmentMenu
{
    public class EquipmentWindow : BaseWindow
    {
        [SerializeField] private Transform _equipmentRoot;

        private IUIFactory _uiFactory;
        
        public void Init(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        private void Start()
        {
            foreach (var weaponType in (WeaponType[]) Enum.GetValues(typeof(WeaponType)))
            {
                _uiFactory.CreateEquipmentItem(weaponType, _equipmentRoot);
            }
        }
    }
}