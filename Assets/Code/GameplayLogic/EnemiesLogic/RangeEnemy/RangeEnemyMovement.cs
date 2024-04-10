using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyMovement : AIState
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private RangeEnemyAnimator _animator;
        
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
            _animator.PlayRunAnimation();
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