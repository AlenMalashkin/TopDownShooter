using System;

namespace Code.Services.TimerService
{
    public interface ITimer
    {
        float TimeRemaining { get; }
        void Tick();
        void StopTimer();
    }
}