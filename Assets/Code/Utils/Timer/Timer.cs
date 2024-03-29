using System;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Utils.Timer
{
    public class Timer : ITimer, IUpdateable
    {
        public event Action<float> TimerChanged;
        public event Action TimerFinished;
        public float TimeRemaining => _timeRemaining;

        private float _timeRemaining;

        public void StartTimer(float timeRemaining)
        {
            _timeRemaining = timeRemaining;
        }

        public void StopTimer()
        {
            TimerFinished?.Invoke();
        }

        public void Update()
        {
            _timeRemaining -= Time.deltaTime;
            TimerChanged?.Invoke(_timeRemaining);

            if (_timeRemaining < 0f)
                TimerFinished?.Invoke();
        }
    }
}