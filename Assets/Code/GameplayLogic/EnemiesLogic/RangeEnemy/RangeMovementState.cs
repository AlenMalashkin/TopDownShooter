using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeMovementState : EnemyMovementState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private Damageable _damageable;
        
        private void OnEnable()
        {
            _playerDetector.PlayerDetected += OnPlayerDetected;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _playerDetector.PlayerDetected -= OnPlayerDetected;
            _damageable.Hit -= OnHit;
        }

        private void OnPlayerDetected()
        {
            _aiStateMachine.EnterState<RangeAttackState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<RangeImpactState>();
        }
    }
}