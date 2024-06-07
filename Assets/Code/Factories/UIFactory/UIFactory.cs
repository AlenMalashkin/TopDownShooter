using Code.Data;
using Code.GameplayLogic.Weapons;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Level;
using Code.Services.AssetProvider;
using Code.Services.ChooseLevelService;
using Code.Services.EquipmentService;
using Code.Services.ProgressService;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
using Code.UI.EquipmentMenu;
using Code.UI.HUD;
using Code.UI.Windows.ChooseLevelWindow;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private IGameStateMachine _gameStateMachine;
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        private IEquipmentService _equipmentService;
        private IChooseLevelService _chooseLevelService;
        private IProgressService _progressService;

        public UIFactory(IGameStateMachine gameStateMachine, IChooseLevelService chooseLevelService,
            IAssetProvider assetProvider, IStaticDataService staticDataService,
            IEquipmentService equipmentService, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _chooseLevelService = chooseLevelService;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _equipmentService = equipmentService;
            _progressService = progressService;
        }

        public GameObject CreateRoot()
        {
            GameObject uiRoot = _assetProvider.LoadAsset("Prefabs/UIRoot");
            return Object.Instantiate(uiRoot);
        }

        public EquipmentItem CreateEquipmentItem(WeaponType type, Transform root)
        {
            EquipmentItem equipmentItemPrefab = _assetProvider.LoadAsset<EquipmentItem>("Prefabs/UI/EquipmentItem");
            WeaponData weaponData = _staticDataService.ForWeapon(type);
            EquipmentItem equipmentItem = Object.Instantiate(equipmentItemPrefab, root);
            equipmentItem.Init(weaponData, _equipmentService, _progressService);
            return equipmentItem;
        }

        public LevelCard CreateLevelCard(LevelType type, Transform root)
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(type);
            LevelCard levelCardPrefab = _assetProvider.LoadAsset<LevelCard>("Prefabs/UI/LevelCard");
            LevelCard levelCard = Object.Instantiate(levelCardPrefab, root);
            levelCard.Init(_gameStateMachine, _chooseLevelService, type, levelStaticData.LevelImage, levelStaticData.LevelName);
            return levelCard;
        }

        public UIJoysticks CreateUIJoysticks(Transform root)
        {
            UIJoysticks uiJoysticks = _assetProvider.LoadAsset<UIJoysticks>("Prefabs/UIJoysticks");
            return Object.Instantiate(uiJoysticks, root);
        }

        public PauseButton CreateUIPauseButton(Transform root)
        {
            PauseButton pauseButton = _assetProvider.LoadAsset<PauseButton>("Prefabs/PauseButton");
            return Object.Instantiate(pauseButton, root);
        }
    }
}