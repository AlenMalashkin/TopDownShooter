using System;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.PlayerLogic;
using Code.Infrastructure;
using Code.Utils.Timer;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class MeleeComboState : AIState
    {
        [SerializeField] private float _commonAttackDamage = 20;
        [SerializeField] private float _ultimateAttackDamage = 40;
        [SerializeField] private float _ultimateAttackCooldown = 3f;
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private AnimatorComponent _meleeEnemyAnimator;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private TriggerObserver _fistTrigger;
        [SerializeField] private Collider _fistCollider;
        [SerializeField] private Damageable _damageable;

        private Timer _timer;
        private IUpdater _updater;

        private float _damage;
        private Transform _target;
        private float _rayLength;

        public void Init(Transform target, IUpdater updater)
        {
            _target = target;
            _updater = updater;
        }

        private void Start()
        {
            _timer = new Timer();

            _updater.Updateables.Add(_timer);
            
            _timer.StartTimer(_ultimateAttackCooldown);
        }

        private void OnEnable()
        {
            _fistTrigger.TriggerEntered += OnAttack;
            _triggerObserver.TriggerLeft += OnTriggerLeft;
            _damageable.Death += OnDeath;
        }

        private void OnDisable()
        {
            _fistTrigger.TriggerEntered -= OnAttack;
            _triggerObserver.TriggerLeft -= OnTriggerLeft;
            _damageable.Death -= OnDeath;
        }

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

            if (_timer.TimeRemaining < 0)
            {
                _meleeEnemyAnimator.PlayAnimationByName(AnimationStrings.UltimateAttack);
                _damage = _ultimateAttackDamage;
            }
            else
            {
                _meleeEnemyAnimator.PlayAnimationByName(AnimationStrings.MeleeCombo);
                _damage = _commonAttackDamage;
            }
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

        private void OnDeath(Damageable damageable)
        {
            _updater.Updateables.Remove(_timer);
        }
    }
}