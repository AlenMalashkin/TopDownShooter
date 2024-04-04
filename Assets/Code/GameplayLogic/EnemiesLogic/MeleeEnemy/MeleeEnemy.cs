using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private EnemyMovement _movement;

        private Transform _target;
        
        public override void Init(Transform followTarget)
        {
            _target = followTarget;
        }

        public override void OnEnable()
        {
            _damageable.Death += _enemyDeath.OnDeath;
        }

        public override void OnDisable()
        {
            _damageable.Death -= _enemyDeath.OnDeath;
        }

        public override void Update()
        {
            if (_target != null)
                _movement.MoveTo(_target);
        }
    }
}