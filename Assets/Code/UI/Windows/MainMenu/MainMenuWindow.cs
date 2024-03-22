using Code.UI.Windows.MainMenu.Buttons;
using UnityEngine;

namespace Code.UI.Windows.MainMenu
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private TestPlayButton _testPlayButton;
        public TestPlayButton TestPlayButton => _testPlayButton;
    }
}