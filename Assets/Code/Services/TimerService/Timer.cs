using System;
using UnityEngine;

namespace Code.Services.TimerService
{
    public class Timer : ITimer
    {
        public float TimeRemaining => _timeRemaining;

        private Action _onTimerFinished;
        private bool _isTicking;
        private float _timeRemaining;

        public Timer(float timeRemaining, Action onTimerFinished = null)
        {
            _timeRemaining = timeRemaining;
            _onTimerFinished = onTimerFinished;
            _isTicking = true;
        }
        
        public void Tick()
        {
            if (!_isTicking)
                return;

            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining < 0f)
            {
                _onTimerFinished?.Invoke();
                _isTicking = false;
            }
        }

        public void StopTimer()
        {
            _isTicking = false;
            _onTimerFinished?.Invoke();
        }
    }
}