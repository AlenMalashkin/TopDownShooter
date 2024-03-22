using Code.Services;
using Code.UI.Windows.MainMenu;

namespace Code.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateRoot();
        MainMenuWindow CreateMainMenu();
    }
}