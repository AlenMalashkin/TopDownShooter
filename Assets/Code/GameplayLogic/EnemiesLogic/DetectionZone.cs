using System;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(BoxCollider))]
    public class DetectionZone : MonoBehaviour
    {
        public UnityAction PlayerDetected;
        public UnityAction PlayerLeft;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                PlayerDetected?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                PlayerLeft?.Invoke();
            }
        }
    }
}