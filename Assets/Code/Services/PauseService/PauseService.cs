using UnityEngine;

namespace Code.Services.PauseService
{
    public class PauseService : IPauseService
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}