using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        [SerializeField] private AIStateMachine _aiStateMachine;

        private Damageable _playerDamageable;

        public void Init(Damageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }
        
        private void Start()
        {
            _damageable.Hit += OnHit;
            _damageable.Death += _deathComponent.OnDeath;
            _playerDetector.PlayerDetected += OnPlayerDetected;
            _playerDetector.PlayerLeft += OnPlayerLeft;
            _playerDamageable.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Hit -= OnHit;
            _damageable.Death -= _deathComponent.OnDeath;
            _playerDetector.PlayerDetected -= OnPlayerDetected;
            _playerDetector.PlayerLeft -= OnPlayerLeft;
            _playerDamageable.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _aiStateMachine.UpdateState();

            if (_aiStateMachine.CurrentStateType == typeof(ImpactState))
                return;
            
            if (_aiStateMachine.CurrentStateType != typeof(EnemyIdleState))
                _playerDetector.CanAttackPlayer();
        }

        private void OnPlayerDetected()
        {
            _aiStateMachine.EnterState<RangeAttackState>();
        }

        private void OnPlayerLeft()
        {
            _aiStateMachine.EnterState<EnemyMovementState>();
        }

        private void OnPlayerDeath(Damageable damageable)
        {
            _aiStateMachine.EnterState<EnemyIdleState>();
        }

        private void OnHit()
            => _aiStateMachine.EnterState<ImpactState>();
    }
}