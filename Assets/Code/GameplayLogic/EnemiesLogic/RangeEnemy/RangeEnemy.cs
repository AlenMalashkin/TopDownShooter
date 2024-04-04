using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private RangeEnemyMovement _movement;
        [SerializeField] private RangeEnemyAttack _attack;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        
        private Transform _target;
        
        public override void Init(Transform followTarget)
        {
            _target = followTarget;
        }

        public override void OnEnable()
        {
            _damageable.Death += _deathComponent.OnDeath;
        }

        public override void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
        }

        public override void Update()
        {
            _movement.MoveTo(_target);
            
            if (_movement.CanAttack)
                _attack.ActivateAttack();
            else
                _attack.DisableAttack();
        }
    }
}