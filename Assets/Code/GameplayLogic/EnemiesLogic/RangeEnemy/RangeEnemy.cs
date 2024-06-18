using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private ParticleSystem _particleSystem;

        private PlayerDeath _playerDeath;

        public void Init(PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;
        }
        
        private void Start()
        {
            _damageable.Death += _deathComponent.OnDeath;
            _damageable.Hit += OnHit;
            _playerDeath.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
            _damageable.Hit -= OnHit;
            _playerDeath.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _playerDetector.CanAttackPlayer();
            _aiStateMachine.UpdateState();
        }

        private void OnPlayerDeath()
            => _aiStateMachine.EnterState<EnemyIdleState>();
        
        private void OnHit()
        {
            _particleSystem.Play();
        }
    }
}