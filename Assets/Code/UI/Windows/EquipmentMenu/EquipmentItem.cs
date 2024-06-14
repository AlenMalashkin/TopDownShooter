using System.Collections.Generic;
using Code.Data;
using Code.Services.EquipmentService;
using Code.Services.ProgressService;
using Code.UI.Windows;
using Code.UI.Windows.MainMenu.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.EquipmentMenu
{
    public class EquipmentItem : BaseButton, IEquipmentObservable, ILocalizable
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _equipStatus;

        private WeaponData _weaponData;
        private IEquipmentService _equipmentService;
        private IProgressService _progressService;
        private Dictionary<string, string> _localization;

        public void Init(WeaponData weaponData, IEquipmentService equipmentService, IProgressService progressService)
        {
            _weaponData = weaponData;
            _equipmentService = equipmentService;
            _progressService = progressService;

            _itemImage.sprite = _weaponData.PreviewSprite;
        }

        protected override void OnClick()
        {
            _equipmentService.EquipWeapon(_weaponData.Type);
        }

        public void UpdateObservable()
        {
            if (_progressService.Progress.CollectedWeapons.Contains(_weaponData.Type))
            {
                _equipStatus.text = _localization["EquipStatus"];
                _button.interactable = true;
            }
            else
            {
                _equipStatus.text = _localization["ClosedStatus"];
                _button.interactable = false;
            }
            
            if (_equipmentService.CurrentEquippedWeapon == _weaponData.Type)
                _equipStatus.text = _localization["EquippedStatus"];
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _localization = localization;
        }
    }
}