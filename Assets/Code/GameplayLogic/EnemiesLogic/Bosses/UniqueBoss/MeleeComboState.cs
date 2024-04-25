using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class MeleeComboState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private AnimatorComponent _meleeEnemyAnimator;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Collider _fistCollider;
        
        private Transform _target;
        private float _rayLength;
        

        public void Init(Transform target)
        {
            _target = target;
        }
        
        private void Start()
        {
            _triggerObserver.TriggerEntered += OnAttack;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnAttack;
        }

        public override void EnterState()
        {
            _meleeEnemyAnimator.PlayAnimationByName(AnimationStrings.MeleeCombo);
        }

        public override void UpdateState()
        {
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        public override void ExitState()
        {
        }

        public void EnableAttackCollider()
            => _fistCollider.enabled = true;
        
        public void DisableAttackCollider()
            => _fistCollider.enabled = false;

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable) && other.TryGetComponent(out Player player))
            {
                damageable.TakeDamage(_damage);
                DisableAttackCollider();
            }
        }
    }
}