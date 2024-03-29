using System;

namespace Code.Utils.Timer
{
    public interface ITimer
    {
        event Action<float> TimerChanged;
        event Action TimerFinished;
        float TimeRemaining { get; }
        void StartTimer(float timeRemaining);
        void StopTimer();
    }
}