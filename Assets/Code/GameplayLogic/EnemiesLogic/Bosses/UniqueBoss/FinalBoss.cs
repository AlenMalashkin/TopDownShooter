using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class FinalBoss : Enemy
    {
        [SerializeField] private DeathComponent _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private ParticleSystem _particleSystem;

        private PlayerDeath _playerDeath;

        public void Init(PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;
        }

        private void Start()
        {
            _damageable.Death += _enemyDeath.OnDeath;
            _damageable.Hit += OnHit;
            _playerDeath.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
            _damageable.Hit -= OnHit;
            _playerDeath.Death -= OnPlayerDeath;
        }

        private void Update()
        {
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