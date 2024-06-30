using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.Bosses.MeleeBoss
{
    public class MeleeBossAttackState : BaseMeleeAttackState
    {
        [SerializeField] private TriggerObserver _attackZoneTrigger;
        [SerializeField] private AIStateMachine _aiStateMachine;

        public override void EnterState()
        {
            _attackZoneTrigger.TriggerLeft += OnTriggerLeft;
            base.EnterState();
        }

        public override void ExitState()
        {
            _attackZoneTrigger.TriggerLeft -= OnTriggerLeft;
            base.ExitState();
        }

        private void OnTriggerLeft(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeBossMovementState>();
        }
    }
}