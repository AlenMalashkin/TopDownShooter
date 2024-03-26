using System;

namespace Code.Services.TimerService
{
    public interface ITimerService : IService
    {
        ITimer StartTimer(float seconds, Action onTimerFinished = null);
        void StopTimer(ITimer timer);
        void Tick();
    }
}