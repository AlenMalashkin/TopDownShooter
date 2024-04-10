using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeMovementState : AIState
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private MeleeEnemyAnimator _meleeEnemyAnimator;

        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }
        
        public override void EnterState()
        {
            _meleeEnemyAnimator.PlayRunAnimation();
        }

        public override void UpdateState()
        {
            if (_target != null)
                MoveTo(_target);
        }

        public override void ExitState()
        {
        }

        private void MoveTo(Transform target)
        {
            _agent.SetDestination(target.position);
        }
    }
}