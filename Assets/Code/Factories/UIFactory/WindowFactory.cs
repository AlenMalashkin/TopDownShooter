using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.StaticDataService;
using Code.UI.EquipmentMenu;
using Code.UI.Windows;
using Code.UI.Windows.LoseWindow;
using Code.UI.Windows.MainMenu;
using Code.UI.Windows.WinWindow;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class WindowFactory : IWindowFactory
    {
        private IStaticDataService _staticDataService;
        private IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;

        public WindowFactory(IStaticDataService staticDataService, IGameStateMachine gameStateMachine,
            IUIFactory uiFactory)
        {
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public MainMenuWindow CreateMainMenu(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.MainMenu).WindowPrefab;
            MainMenuWindow menuWindow = Object.Instantiate(window, root) as MainMenuWindow;
            menuWindow.TestPlayButton.Init(_gameStateMachine);
            menuWindow.EquipmentButton.Init(this, root);
            return menuWindow;
        }

        public GameObject CreateLoseWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.LoseWindow).WindowPrefab;
            LoseWindow loseWindow = Object.Instantiate(window, root) as LoseWindow;
            loseWindow.Init(_gameStateMachine);
            return loseWindow.gameObject;
        }

        public GameObject CreateWinWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.WinWindow).WindowPrefab;
            WinWindow winWindow = Object.Instantiate(window, root) as WinWindow;
            winWindow.Init(_gameStateMachine);
            return winWindow.gameObject;
        }

        public GameObject CreateEquipmentWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.EquipmentWindow).WindowPrefab;
            EquipmentWindow equipmentWindow = Object.Instantiate(window, root) as EquipmentWindow;
            equipmentWindow.Init(_uiFactory);
            return equipmentWindow.gameObject;
        }
    }
}