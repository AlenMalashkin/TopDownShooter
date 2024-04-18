using Code.UI.Windows.MainMenu;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IWindowFactory : IFactory
    {
        MainMenuWindow CreateMainMenu(Transform root);
        GameObject CreateLoseWindow(Transform root);
        GameObject CreateWinWindow(Transform root);
    }
}