using Code.Data;
using Code.Services.EquipmentService;
using Code.Services.ProgressService;
using Code.UI.Windows.MainMenu.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.EquipmentMenu
{
    public class EquipmentItem : BaseButton, IEquipmentObservable
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _equipStatus;

        private WeaponData _weaponData;
        private IEquipmentService _equipmentService;
        private IProgressService _progressService;

        public void Init(WeaponData weaponData, IEquipmentService equipmentService, IProgressService progressService)
        {
            _weaponData = weaponData;
            _equipmentService = equipmentService;
            _progressService = progressService;

            _itemImage.sprite = _weaponData.PreviewSprite;
            _itemName.text = _weaponData.ItemName;
        }

        protected override void OnClick()
        {
            _equipmentService.EquipWeapon(_weaponData.Type);
        }

        public void UpdateObservable()
        {
            if (_progressService.Progress.CollectedWeapons.Contains(_weaponData.Type))
            {
                _equipStatus.text = "Экипировать";
                _button.interactable = true;
            }
            else
            {
                _equipStatus.text = "Закрыто";
                _button.interactable = false;
            }
            
            if (_equipmentService.CurrentEquippedWeapon == _weaponData.Type)
                _equipStatus.text = "Экипировано";
        }
    }
}