using Code.GameplayLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.Tutorial.TutorialWindows;
using Code.UI.Pause;
using Code.UI.Windows.ChooseLevelWindow;
using Code.UI.Windows.MainMenu;
using Code.UI.Windows.RewardWindow;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IWindowFactory : IFactory
    {
        MainMenuWindow CreateMainMenu(Transform root);
        GameObject CreateLoseWindow(Transform root);
        GameObject CreateWinWindow(Transform root);
        GameObject CreateEquipmentWindow(Transform root);
        ChooseLevelWindow CreateChooseLevelWindow(Transform root);
        TutorialPassedWindow CreateTutorialWindow(Transform root);
        TutorialDialogWindow CreateTutorialDialogWindow(Transform root);
        void CreatePauseWindow(Transform root);
        RewardWindow CreateRewardWindow(Transform root, Damageable damageable, PlayerDeath playerDeath);
    }
}