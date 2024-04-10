using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        [SerializeField] private AIStateMachine _aiStateMachine;

        public override void OnEnable()
        {
            _damageable.Death += _deathComponent.OnDeath;
        }

        public override void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
        }

        public override void Update()
        {
            _aiStateMachine.UpdateState();
            
            _aiStateMachine.EnterState<RangeEnemyMovement>();
            
            if (_playerDetector.CanAttackPlayer())
                _aiStateMachine.EnterState<RangeEnemyAttack>();
        }
    }
}