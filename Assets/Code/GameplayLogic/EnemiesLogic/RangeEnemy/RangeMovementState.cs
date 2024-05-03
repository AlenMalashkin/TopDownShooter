using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeMovementState : AIState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimatorComponent _enemyAnimator;
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private Damageable _damageable;
        
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }

        private void OnEnable()
        {
            _playerDetector.PlayerDetected += OnPlayerDetected;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _playerDetector.PlayerDetected -= OnPlayerDetected;
            _damageable.Hit -= OnHit;
        }

        public override void EnterState()
        {
            _agent.isStopped = false;
            _enemyAnimator.PlayAnimationByName(AnimationStrings.Run);
        }

        public override void UpdateState()
        {
            MoveTo(_target);
        }

        public override void ExitState()
        {
            _agent.isStopped = true;
        }

        private void MoveTo(Transform target)
        {
            _agent.SetDestination(target.position);
        }

        private void OnPlayerDetected()
        {
            _aiStateMachine.EnterState<RangeAttackState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<RangeImpactState>();
        }
    }
}