using Code.Services.InputService;
using UnityEngine;

namespace Code.Services.PauseService
{
    public class PauseService : IPauseService
    {
        public bool Paused => _paused;
        
        private bool _paused;
        private IInputService _inputService;

        public PauseService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Pause()
        {
            _paused = true;
            Time.timeScale = 0;
            _inputService.Disable();
        }

        public void Resume()
        {
            _paused = false;
            Time.timeScale = 1;
            _inputService.Enable();
        }
    }
}