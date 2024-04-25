using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class UniqueEnemy : Enemy
    {
        [SerializeField] private DeathComponent _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private AIStateMachineBase _aiStateMachine;
        [SerializeField] private TriggerObserver _triggerObserver;

        private Damageable _playerDamageable;

        public void Init(Damageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }

        private void Start()
        {
            _damageable.Death += _enemyDeath.OnDeath;
            _triggerObserver.TriggerEntered += OnTriggerEntered;
            _triggerObserver.TriggerLeft += OnTriggerLeft;
            _playerDamageable.Death += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
            _triggerObserver.TriggerLeft -= OnTriggerLeft;
            _playerDamageable.Death -= OnPlayerDeath;
        }

        private void Update()
        {
            _aiStateMachine.UpdateState();
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeComboState>();
        }

        private void OnTriggerLeft(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _aiStateMachine.EnterState<MeleeComboState>();
        }

        private void OnPlayerDeath(Damageable damageable)
            => _aiStateMachine.EnterState<EnemyIdleState>();
    }
}