using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.ChooseLevelService;
using Code.Services.EquipmentService;
using Code.Services.PauseService;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;
using Code.Tutorial.TutorialWindows;
using Code.UI.EquipmentMenu;
using Code.UI.Pause;
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
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IPauseService _pauseService;
        private IEquipmentService _equipmentService;

        public WindowFactory(IStaticDataService staticDataService, IGameStateMachine gameStateMachine,
            IUIFactory uiFactory, IChooseLevelService chooseLevelService, IProgressService progressService,
            ISaveLoadService saveLoadService, IPauseService pauseService, IEquipmentService equipmentService)
        {
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _chooseLevelService = chooseLevelService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _pauseService = pauseService;
            _equipmentService = equipmentService;
        }

        public MainMenuWindow CreateMainMenu(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.MainMenu).WindowPrefab;
            MainMenuWindow menuWindow = Object.Instantiate(window, root) as MainMenuWindow;
            menuWindow.ChooseLevelButton.Init(_gameStateMachine, this, _progressService, root);
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
            winWindow.Init(_gameStateMachine, _chooseLevelService);
            return winWindow.gameObject;
        }

        public GameObject CreateEquipmentWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.EquipmentWindow).WindowPrefab;
            EquipmentWindow equipmentWindow = Object.Instantiate(window, root) as EquipmentWindow;
            equipmentWindow.Init(_uiFactory, _equipmentService);
            return equipmentWindow.gameObject;
        }

        public ChooseLevelWindow CreateChooseLevelWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.ChooseLevelWindow).WindowPrefab;
            ChooseLevelWindow chooseLevelWindow = Object.Instantiate(window, root) as ChooseLevelWindow;
            chooseLevelWindow.Init(_uiFactory);
            return chooseLevelWindow;
        }

        public TutorialPassedWindow CreateTutorialWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.TutorialPassWindow).WindowPrefab;
            TutorialPassedWindow tutorialPassedWindow = Object.Instantiate(window, root) as TutorialPassedWindow;
            tutorialPassedWindow.Init(_gameStateMachine, _progressService, _saveLoadService);
            return tutorialPassedWindow;
        }

        public TutorialDialogWindow CreateTutorialDialogWindow(Transform root)
        {
            BaseWindow window = _staticDataService.ForWindow(WindowType.TutorialMessageWindow).WindowPrefab;
            TutorialDialogWindow tutorialDialogWindow = Object.Instantiate(window, root) as TutorialDialogWindow;
            return tutorialDialogWindow;
        }

        public PauseWindow CreatePauseWindow(Transform root)
        {
            PauseWindow pauseWindow =
                Object.Instantiate(_staticDataService.ForWindow(WindowType.PauseWindow).WindowPrefab, root) as
                    PauseWindow;
            pauseWindow.Init(_pauseService, _gameStateMachine);
            return pauseWindow;
        }
    }
}