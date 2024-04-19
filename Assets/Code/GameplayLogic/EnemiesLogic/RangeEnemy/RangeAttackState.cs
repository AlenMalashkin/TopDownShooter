using Code.GameplayLogic.Weapons;
using UnityEngine;
namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeAttackState : AIState
    {
        [SerializeField] private Transform _enemyArm;
        [SerializeField] private AnimatorComponent _animator;

        public Transform EnemyArm => _enemyArm;

        private Weapon _weapon;
        private Transform _target;

        public void Init(Weapon weapon, Transform target)
        {
            _weapon = weapon;
            _target = target;
        }

        public override void EnterState()
        {
            _animator.PlayAnimationByName(AnimationStrings.Attack);
        }

        public override void UpdateState()
        {
            _weapon.ShootBullet(transform.forward);
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        public override void ExitState()
        {
        }
    }
}