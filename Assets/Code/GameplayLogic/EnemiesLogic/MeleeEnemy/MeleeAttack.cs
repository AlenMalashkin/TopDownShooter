using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeAttack : EnemyAttack
    {
        [SerializeField] private PlayerDetectionZone playerDetectionZone;
        [SerializeField] private Collider _fistCollider;
        
        private void OnEnable()
        {
            playerDetectionZone.PlayerDetected += OnAttack;
        }

        private void OnDisable()
        {
            playerDetectionZone.PlayerDetected -= OnAttack;
        }

        public override void ActivateAttack()
            => _fistCollider.enabled = true;

        public override void DisableAttack()
            => _fistCollider.enabled = false;

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable))
                damageable.TakeDamage(Damage);
        }
    }
}