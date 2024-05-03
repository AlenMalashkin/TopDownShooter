using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.PlayerLogic;
using Code.Utils.Timer;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class MeleeComboState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private float _ultimateAttackCooldown = 3f;
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private AnimatorComponent _meleeEnemyAnimator;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private TriggerObserver _fistTrigger;
        [SerializeField] private Collider _fistCollider;

        private Timer _timer;

        private Transform _target;
        private float _rayLength;


        public void Init(Transform target)
        {
            _target = target;
        }

        private void Awake()
        {
            _timer = new Timer();
        }

        private void OnEnable()
        {
            _fistTrigger.TriggerEntered += OnAttack;
            _triggerObserver.TriggerLeft += OnTriggerLeft;
        }

        private void OnDisable()
        {
            _fistTrigger.TriggerEntered -= OnAttack;
            _triggerObserver.TriggerLeft -= OnTriggerLeft;
        }

        public override void EnterState()
        {
            _timer.StartTimer(_ultimateAttackCooldown);
        }

        public override void UpdateState()
        {
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
            _timer.Update();

            _meleeEnemyAnimator.PlayAnimationByName(_timer.TimeRemaining < 0
                ? AnimationStrings.UltimateAttack
                : AnimationStrings.MeleeCombo);
        }

        public override void ExitState()
        {
        }

        public void EnableAttackCollider()
            => _fistCollider.enabled = true;

        public void DisableAttackCollider()
            => _fistCollider.enabled = false;

        public void OnUltimateAttackFinished()
            => _timer.StartTimer(_ultimateAttackCooldown);

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable) && other.TryGetComponent(out Player player))
            {
                damageable.TakeDamage(_damage);
            }
        }

        private void OnTriggerLeft(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _aiStateMachine.EnterState<FinalBossMovementState>();
            }
        }
    }
}