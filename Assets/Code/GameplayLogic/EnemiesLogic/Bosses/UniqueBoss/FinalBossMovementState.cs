using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.Bosses
{
    public class FinalBossMovementState : AIState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimatorComponent _enemyAnimator;
        [SerializeField] private TriggerObserver _triggerObserver;

        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
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

        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeComboState>();
        }
    }
}