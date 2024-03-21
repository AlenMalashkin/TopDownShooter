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
                if (value > _maxHealth)
                {
                    _health = _maxHealth;
                }
                else if (_health < 0)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.Log("hit");
        }
    }
}