using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeAttackState : BaseMeleeAttackState
    {
        [SerializeField] private TriggerObserver _attackZoneTrigger;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachine _aiStateMachine;

        public override void EnterState()
        {
            _attackZoneTrigger.TriggerLeft += OnTriggerLeft;
            _damageable.Hit += OnHit;
            base.EnterState();
        }

        public override void ExitState()
        {
            _attackZoneTrigger.TriggerLeft -= OnTriggerLeft;
            _damageable.Hit -= OnHit;
            base.ExitState();
        }

        private void OnTriggerLeft(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeMovementState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<MeleeImpactState>();
        }
    }
}