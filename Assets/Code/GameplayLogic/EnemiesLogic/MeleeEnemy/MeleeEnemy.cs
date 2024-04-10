using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private PlayerDetectionZone _playerDetectionZone;

        public override void OnEnable()
        {
            _damageable.Death += _enemyDeath.OnDeath;
            _playerDetectionZone.PlayerDetected += OnPlayerDetected;
            _playerDetectionZone.PlayerLeft += OnPlayerLeft;
        }

        public override void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
            _playerDetectionZone.PlayerDetected -= OnPlayerDetected;
            _playerDetectionZone.PlayerLeft -= OnPlayerLeft;
        }

        public override void Update()
        {
            _aiStateMachine.UpdateState();
        }

        private void OnPlayerDetected(Collider other)
            => _aiStateMachine.EnterState<MeleeAttackState>();

        private void OnPlayerLeft(Collider other)
            => _aiStateMachine.EnterState<MeleeMovementState>();
    }
}