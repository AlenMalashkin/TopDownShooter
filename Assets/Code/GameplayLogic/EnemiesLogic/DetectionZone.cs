using UnityEngine;
using UnityEngine.Events;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(BoxCollider))]
    public class DetectionZone : MonoBehaviour
    {
        public static UnityAction PlayerDetected;

        private BoxCollider _collider;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerDetected?.Invoke();
            }
        }
    }
}