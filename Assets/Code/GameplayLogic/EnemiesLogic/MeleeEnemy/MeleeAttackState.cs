using Code.Audio;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeAttackState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private AudioClip _punchSound;
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private AnimatorComponent _meleeEnemyAnimator;
        [SerializeField] private TriggerObserver _fistTriggerObserver;
        [SerializeField] private TriggerObserver _attackZoneTrigger;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Collider _fistCollider;
        
        private Transform _target;
        private float _rayLength;

        public void Init(Transform target)
        {
            _target = target;
        }
        
        private void OnEnable()
        {
            _fistTriggerObserver.TriggerEntered += OnAttack;
            _attackZoneTrigger.TriggerLeft += OnTriggerLeft;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _fistTriggerObserver.TriggerEntered -= OnAttack;
            _attackZoneTrigger.TriggerLeft -= OnTriggerLeft;
            _damageable.Hit -= OnHit;
        }

        public override void EnterState()
        {
            _meleeEnemyAnimator.PlayAnimationByName(AnimationStrings.Attack);
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

        public void EnableFistCollider()
            => _fistCollider.enabled = true;
        
        public void DisableFistCollider()
            => _fistCollider.enabled = false;

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable) && other.TryGetComponent(out Player player))
            {
                _soundPlayer.Play(_punchSound);
                damageable.TakeDamage(_damage);
                DisableFistCollider();
            }
        }

        private void OnTriggerLeft(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeMovementState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<MeleeImpactState>();
        }
    }
}