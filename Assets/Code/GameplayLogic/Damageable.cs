using UnityEngine;

namespace Code.GameplayLogic
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        
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
        }
    }
}