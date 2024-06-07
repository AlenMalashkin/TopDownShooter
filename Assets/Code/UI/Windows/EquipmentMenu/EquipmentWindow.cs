using System;
using System.Collections.Generic;
using Code.Factories.UIFactory;
using Code.GameplayLogic.Weapons;
using Code.Services.EquipmentService;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.EquipmentMenu
{
    public class EquipmentWindow : BaseWindow, IEquipmentObserver
    {
        [SerializeField] private Transform _equipmentRoot;

        private List<IEquipmentObservable> _shopItems = new List<IEquipmentObservable>();

        private IUIFactory _uiFactory;
        private IEquipmentService _equipmentService;

        public void Init(IUIFactory uiFactory, IEquipmentService equipmentService)
        {
            _uiFactory = uiFactory;
            _equipmentService = equipmentService;
        }

        private void Start()
        {
            foreach (var weaponType in (WeaponType[]) Enum.GetValues(typeof(WeaponType)))
            {
                _shopItems.Add(_uiFactory.CreateEquipmentItem(weaponType, _equipmentRoot)
                    .GetComponent<IEquipmentObservable>());
            }
            
            Observe();
            
            _equipmentService.WeaponEquipped += OnWeaponEquipment;
        }

        private void OnDisable()
        {
            _equipmentService.WeaponEquipped -= OnWeaponEquipment;
        }

        public void Observe()
        {
            foreach (var observable in _shopItems)
            {
                observable.UpdateObservable();
            }
        }

        private void OnWeaponEquipment(WeaponType type)
        {
            Observe();
        }
    }
}