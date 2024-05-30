using System;
using Code.GameplayLogic;
using Code.Services.EnemiesProvider;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialEnemy : Enemy
    {
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Collider _hitBox;

        private IEnemiesProvider _enemiesProvider;

        public void Init(IEnemiesProvider enemiesProvider)
        {
            _enemiesProvider = enemiesProvider;
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
            _animator.PlayAnimationByName("Death");
            _enemiesProvider.RemoveEnemy(this);
            Destroy(gameObject, 3);
        }
    }
}