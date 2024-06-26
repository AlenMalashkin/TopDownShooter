using Code.GameplayLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeMovementState : EnemyMovementState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Damageable _damageable;

        private void OnEnable()
        {
            _triggerObserver.TriggerEntered += OnTriggerEntered;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
            _damageable.Hit -= OnHit;
        }
        
        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeAttackState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<MeleeImpactState>();
        }
    }
}