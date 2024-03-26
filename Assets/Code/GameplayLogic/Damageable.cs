using System;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        public event Action Death;

        private void OnValidate()
        {
            if (_health > _maxHealth)
            {
                _maxHealth = _health + 1;
            }
        }

        public int Health
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

        public void TakeDamage(int damage)
        {
            Health -= damage;
            
            if (_health <= 0)
                Death?.Invoke();
        }
    }
}