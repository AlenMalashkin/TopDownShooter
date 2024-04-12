using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private PlayerDetectionZone _playerDetectionZone;

        private Damageable _playerDamageable;
        
        public void Init(Damageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }
        
        private void Start()
        {
            _damageable.Death += _enemyDeath.OnDeath;
            _playerDetectionZone.PlayerDetected += OnPlayerDetected;
            _playerDetectionZone.PlayerLeft += OnPlayerLeft;
            _playerDamageable.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
            _playerDetectionZone.PlayerDetected -= OnPlayerDetected;
            _playerDetectionZone.PlayerLeft -= OnPlayerLeft;
            _playerDamageable.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _aiStateMachine.UpdateState();
        }

        private void OnPlayerDetected(Collider other)
            => _aiStateMachine.EnterState<MeleeAttackState>();

        private void OnPlayerLeft(Collider other)
            => _aiStateMachine.EnterState<EnemyMovementState>();

        private void OnPlayerDeath()
            => _aiStateMachine.EnterState<EnemyIdleState>();
    }
}