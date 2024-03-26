using System;
using System.Collections.Generic;

namespace Code.Services.TimerService
{
    public class TimerService : ITimerService
    {
        private List<ITimer> _timers = new List<ITimer>();
        
        public ITimer StartTimer(float seconds, Action onTimerFinished = null)
        {
            ITimer timer = new Timer(seconds, onTimerFinished);
            _timers.Add(timer);
            return timer;
        }

        public void StopTimer(ITimer timer)
        {
            timer.StopTimer();
            _timers.Remove(timer);
        }

        public void Tick()
        {
            foreach (var timer in _timers)
            {
                timer.Tick();
            }
        }
    }
}