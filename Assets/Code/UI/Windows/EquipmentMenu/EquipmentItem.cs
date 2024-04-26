using Code.Data;
using Code.Services.EquipmentService;
using Code.UI.Windows.MainMenu.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.EquipmentMenu
{
    public class EquipmentItem : BaseButton
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemName;
        
        private WeaponData _weaponData;
        private IEquipmentService _equipmentService;
        
        public void Init(WeaponData weaponData, IEquipmentService equipmentService)
        {
            _weaponData = weaponData;
            _equipmentService = equipmentService;

            _itemImage.sprite = _weaponData.PreviewSprite;
            _itemName.text = _weaponData.ItemName;
        }

        protected override void OnClick()
        {
            _equipmentService.EquipWeapon(_weaponData.Type);
        }
    }
}