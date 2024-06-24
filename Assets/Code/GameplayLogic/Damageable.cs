using System;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<float> HealthChanged;
        public event Action Hit;
        public event Action<Damageable> Death;
        
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        private AnimatorComponent _animator;
        private bool _died;

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
                    _died = true;
                    _health = 0;
                }
                else if(_health > 0)
                {
                    _died = false;
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
            Hit?.Invoke();
            
            if (_died)
                return;

            Health -= damage;

            HealthChanged?.Invoke(Health);

            if (_health <= 0)
                Death?.Invoke(this);
        }

        public void Heal(float heal)
        {
            if (Health + heal > MaxHealth)
                Health = MaxHealth;
            else
                Health += heal;

            HealthChanged?.Invoke(_health);
        }
    }
}