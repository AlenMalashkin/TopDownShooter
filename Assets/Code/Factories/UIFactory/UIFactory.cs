using Code.Data;
using Code.GameplayLogic.Weapons;
using Code.Services.AssetProvider;
using Code.Services.EquipmentService;
using Code.Services.StaticDataService;
using Code.UI.EquipmentMenu;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        private IEquipmentService _equipmentService;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService,
            IEquipmentService equipmentService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _equipmentService = equipmentService;
        }

        public GameObject CreateRoot()
        {
            GameObject uiRoot = _assetProvider.LoadAsset("Prefabs/UIRoot");
            return Object.Instantiate(uiRoot);
        }

        public GameObject CreateEquipmentItem(WeaponType type, Transform root)
        {
            EquipmentItem equipmentItemPrefab = _assetProvider.LoadAsset<EquipmentItem>("Prefabs/UI/EquipmentItem");
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            EquipmentItem equipmentItem = Object.Instantiate(equipmentItemPrefab, root);
            equipmentItem.Init(weaponData, _equipmentService);
            return equipmentItem.gameObject;
        }
    }
}