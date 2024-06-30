using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.Bosses.MeleeBoss
{
    public class MeleeBossMovementState : EnemyMovementState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _triggerObserver.TriggerEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
        }
        
        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeBossAttackState>();
        }
    }
}