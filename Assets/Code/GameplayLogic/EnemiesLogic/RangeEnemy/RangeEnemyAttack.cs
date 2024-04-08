using Code.GameplayLogic.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyAttack : EnemyAttack
    {
        [SerializeField] private Transform _enemyArm;

        public Transform EnemyArm => _enemyArm;

        private NavMeshAgent _agent;
        private RangeEnemyAnimator _animator;
        private Weapon _weapon;

        public void Init(Weapon weapon)
        {
            _weapon = weapon;
        }

        private void Awake()
        {
            _animator = GetComponent<RangeEnemyAnimator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void ActivateAttack()
        {
            _animator.PlayAttackAnimation();
            _agent.isStopped = true;
            _weapon.ShootBullet(transform.forward);
        }

        public override void DisableAttack()
        {
            _agent.isStopped = false;
        }
    }
}