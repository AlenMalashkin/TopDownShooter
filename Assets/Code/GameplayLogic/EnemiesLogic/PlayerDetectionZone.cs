using System;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class PlayerDetectionZone : MonoBehaviour
    {
        public event Action<Collider> PlayerDetected;
        public event Action<Collider> PlayerLeft;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                PlayerDetected?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                PlayerLeft?.Invoke(other);
            }
        }
    }
}