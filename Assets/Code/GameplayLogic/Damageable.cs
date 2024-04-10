using System;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        private AnimatorComponent _animator;

        public event Action<float> HealthChanged;
        public event Action Hit; 
        public event Action Death;

        private void Awake()
        {
            _animator = GetComponent<AnimatorComponent>();
            _health = _maxHealth;
        }

        private void OnValidate()
        {
            if (_health > _maxHealth)
            {
                _maxHealth = _health + 1;
            }
        }

        public float Health
        {
            get => _health;
            private set
            {
                _health = value;
                if (_health < 0)
                {
                    _health = 0;
                }
                else if (_health > _maxHealth)
                {
                    _health = _maxHealth;
                }
            }
        }

        public float MaxHealth => _maxHealth;
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
            
            HealthChanged?.Invoke(Health);
            
            if (_health <= 0)
                Death?.Invoke();
            
            Hit?.Invoke();
        }
        
    }
}