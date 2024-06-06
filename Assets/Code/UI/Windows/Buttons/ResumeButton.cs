using Code.Services.PauseService;
using Code.UI.Windows.MainMenu.Buttons;

namespace Code.UI.Windows.Buttons
{
    public class ResumeButton : BaseButton
    {
        private IClosableWindow _window;
        private IPauseService _pauseService;

        public void Init(IPauseService pauseService, IClosableWindow window)
        {
            _window = window;
            _pauseService = pauseService;
        }
        
        protected override void OnClick()
        {
            _window.Close();
            _pauseService.Resume();
        }
    }
}