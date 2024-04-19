using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services;
using Code.Services.StaticDataService;
using Code.UI.Windows;
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

        public WindowFactory(IStaticDataService staticDataService, IGameStateMachine gameStateMachine)
        {
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public MainMenuWindow CreateMainMenu(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.MainMenu).WindowPrefab;
            MainMenuWindow menuWindow = Object.Instantiate(window, root) as MainMenuWindow;
            menuWindow.TestPlayButton.Init(_gameStateMachine);
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
    }
}