using Code.GameplayLogic.EnemiesLogic;
using Code.Pickups;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _damageable.Death += _deathComponent.OnDeath;
            _triggerObserver.TriggerEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Pickup pickup))
            {
                pickup.PickupItem();
            }
        }
    }
}