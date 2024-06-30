using Code.GameplayLogic.EnemiesLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic
{
    public class EnemyMovementState : AIState
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimatorComponent _enemyAnimator;

        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
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
    }
}