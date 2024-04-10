using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeAttackState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private MeleeEnemyAnimator _meleeEnemyAnimator;
        [SerializeField] private PlayerDetectionZone _playerDetectionZone;
        [SerializeField] private Collider _fistCollider;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }
        
        private void OnEnable()
        {
            _playerDetectionZone.PlayerDetected += OnAttack;
        }

        private void OnDisable()
        {
            _playerDetectionZone.PlayerDetected -= OnAttack;
        }

        public override void EnterState()
        {
            _meleeEnemyAnimator.PlayAttackAnimation();
        }

        public override void UpdateState()
        {
            transform.LookAt(_target);
        }

        public override void ExitState()
        {
        }

        public void EnableFistCollider()
            => _fistCollider.enabled = true;
        
        public void DisableFistCollider()
            => _fistCollider.enabled = false;

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}