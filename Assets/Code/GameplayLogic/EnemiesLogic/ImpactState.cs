using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using Code.Utils.Timer;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class ImpactState : AIState
    {
        [SerializeField] private float _stunTime = 0.5f;
        
        private AIStateMachine _aiStateMachine;
        private AnimatorComponent _animator;
        private NavMeshAgent _agent;
        private Timer _timer;

        private void Awake()
        {
            _aiStateMachine = GetComponent<AIStateMachine>();
            _animator = GetComponent<AnimatorComponent>();
            _agent = GetComponent<NavMeshAgent>();
            _timer = new Timer();
        }

        private void OnEnable()
        {
            _timer.TimerFinished += OnTimerFinished;
        }

        private void OnDisable()
        {
            _timer.TimerFinished -= OnTimerFinished;
        }

        public override void EnterState()
        {
            _timer.StartTimer(_stunTime);
            _animator.PlayAnimationByName("Impact");
            _agent.isStopped = true;
        }

        public override void UpdateState()
        {
            _timer.Update();
        }

        public override void ExitState()
        {
            _agent.isStopped = false;
        }

        private void OnTimerFinished()
        {
            _aiStateMachine.EnterState<EnemyMovementState>();
        }
    }
}