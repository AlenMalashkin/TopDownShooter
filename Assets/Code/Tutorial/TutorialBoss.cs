using Code.Factories.GameplayFactoies;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialBoss : Enemy
    {
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Collider _hitBox;

        private IPickupFactory _pickupFactory;

        public void Init(IPickupFactory pickupFactory)
        {
            _pickupFactory = pickupFactory;
        }
        
        private void OnEnable()
        {
            _damageable.HealthChanged += OnHealthChanged;
            _damageable.Death += OnDeath;
        }

        private void OnDisable()
        {
            _damageable.HealthChanged -= OnHealthChanged;
            _damageable.Death -= OnDeath;
        }

        private void OnHealthChanged(float health)
        {
            _animator.PlayAnimationByName("Impact");
        }

        private void OnDeath(Damageable damageable)
        {
            _hitBox.enabled = false;
            _pickupFactory.CrateTutorialPickup(transform.position, WeaponType.Pistol);
            _animator.PlayAnimationByName("Death");
            Destroy(gameObject, 3);
        }
    }
}