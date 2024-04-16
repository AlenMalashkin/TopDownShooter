using System;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerLeft;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerLeft?.Invoke(other);
        }
    }
}