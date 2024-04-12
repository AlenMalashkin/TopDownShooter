using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class EnemyIdleState : AIState
    {
        [SerializeField] private AnimatorComponent _animator;
        
        public override void EnterState()
        {
            _animator.PlayAnimationByName("Idle");
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}