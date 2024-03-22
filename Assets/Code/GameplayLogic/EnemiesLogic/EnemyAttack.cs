using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) &&
                other.TryGetComponent(out PlayerMovement playerMovement))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}