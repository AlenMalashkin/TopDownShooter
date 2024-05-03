using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private DeathComponent _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachineBase _aiStateMachine;

        private Damageable _playerDamageable;

        public void Init(Damageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }

        private void Start()
        {
            _damageable.Death += _enemyDeath.OnDeath;
            _playerDamageable.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
            _playerDamageable.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _aiStateMachine.UpdateState();
        }

        private void OnPlayerDeath(Damageable damageable)
            => _aiStateMachine.EnterState<EnemyIdleState>();
    }
}