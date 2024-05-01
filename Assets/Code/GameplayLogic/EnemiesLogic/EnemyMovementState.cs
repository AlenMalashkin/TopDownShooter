using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
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
        }

        public override void UpdateState()
        {
            MoveTo(_target);
            _enemyAnimator.PlayAnimationByName(AnimationStrings.Run);
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