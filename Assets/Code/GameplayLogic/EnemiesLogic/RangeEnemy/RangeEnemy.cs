using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;

        private Damageable _playerDamageable;

        public void Init(Damageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }
        
        private void Start()
        {
            _damageable.Death += _deathComponent.OnDeath;
            _playerDamageable.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
            _playerDamageable.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _playerDetector.CanAttackPlayer();
            _aiStateMachine.UpdateState();
        }

        private void OnPlayerDeath(Damageable damageable)
            => _aiStateMachine.EnterState<EnemyIdleState>();
    }
}