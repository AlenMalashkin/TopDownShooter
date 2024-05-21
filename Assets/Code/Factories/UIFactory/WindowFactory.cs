using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ChooseLevelService;
using Code.Services.StaticDataService;
using Code.UI.EquipmentMenu;
using Code.UI.Windows;
using Code.UI.Windows.ChooseLevelWindow;
using Code.UI.Windows.LoseWindow;
using Code.UI.Windows.MainMenu;
using Code.UI.Windows.WinWindow;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class WindowFactory : IWindowFactory
    {
        private IStaticDataService _staticDataService;
        private IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        private IChooseLevelService _chooseLevelService;

        public WindowFactory(IStaticDataService staticDataService, IGameStateMachine gameStateMachine,
            IUIFactory uiFactory, IChooseLevelService chooseLevelService)
        {
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _chooseLevelService = chooseLevelService;
        }

        public MainMenuWindow CreateMainMenu(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.MainMenu).WindowPrefab;
            MainMenuWindow menuWindow = Object.Instantiate(window, root) as MainMenuWindow;
            menuWindow.TestPlayButton.Init(_gameStateMachine);
            menuWindow.EquipmentButton.Init(this, root);
            menuWindow.ChooseLevelButton.Init(this, root);
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
            winWindow.Init(_gameStateMachine, _chooseLevelService);
            return winWindow.gameObject;
        }

        public GameObject CreateEquipmentWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.EquipmentWindow).WindowPrefab;
            EquipmentWindow equipmentWindow = Object.Instantiate(window, root) as EquipmentWindow;
            equipmentWindow.Init(_uiFactory);
            return equipmentWindow.gameObject;
        }

        public ChooseLevelWindow CreateChooseLevelWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.ChooseLevelWindow).WindowPrefab;
            ChooseLevelWindow chooseLevelWindow = Object.Instantiate(window, root) as ChooseLevelWindow;
            chooseLevelWindow.Init(_uiFactory);
            return chooseLevelWindow;
        }
    }
}