using Code.GameplayLogic.Weapons;
using UnityEngine;
namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeAttackState : AIState
    {
        [SerializeField] private AIStateMachine _aiStateMachine;
        [SerializeField] private RangeEnemyPlayerDetector _playerDetector;
        [SerializeField] private Transform _enemyArm;
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Damageable _damageable;

        public Transform EnemyArm => _enemyArm;

        private Weapon _weapon;
        private Transform _target;

        public void Init(Weapon weapon, Transform target)
        {
            _weapon = weapon;
            _target = target;
        }

        private void OnEnable()
        {
            _playerDetector.PlayerLeft += OnPlayerLeft;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _playerDetector.PlayerLeft -= OnPlayerLeft;
            _damageable.Hit -= OnHit;
        }

        public override void EnterState()
        {
            _animator.PlayAnimationByName(AnimationStrings.Attack);
        }

        public override void UpdateState()
        {
            _weapon.Shoot(transform.forward);
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        public override void ExitState()
        {
        }

        private void OnPlayerLeft()
        {
            _aiStateMachine.EnterState<RangeMovementState>();
        }

        private void OnHit()
        {
            _aiStateMachine.EnterState<RangeImpactState>();
        }
    }
}