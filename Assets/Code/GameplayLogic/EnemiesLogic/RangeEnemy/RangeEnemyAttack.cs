using Code.GameplayLogic.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyAttack : AIState
    {
        [SerializeField] private Transform _enemyArm;

        public Transform EnemyArm => _enemyArm;

        private RangeEnemyAnimator _animator;
        private Weapon _weapon;
        private Transform _target;

        public void Init(Weapon weapon, Transform target)
        {
            _weapon = weapon;
            _target = target;
        }

        private void Awake()
        {
            _animator = GetComponent<RangeEnemyAnimator>();
        }

        public override void EnterState()
        {
            _animator.PlayAttackAnimation();
        }

        public override void UpdateState()
        {
            _weapon.ShootBullet(transform.forward);
            transform.LookAt(_target);
        }

        public override void ExitState()
        {
        }
    }
}