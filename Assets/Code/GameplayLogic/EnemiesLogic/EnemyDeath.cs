using Code.Services.EnemiesProvider;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{

    public class EnemyDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Enemy _enemy;

        private IEnemiesProvider _enemiesProvider;

        public void Init(IEnemiesProvider enemiesProvider)
        {
            _enemiesProvider = enemiesProvider;
        }

        public override void OnDeath(Damageable damageable)
        {
            base.OnDeath(damageable);

            _enemiesProvider.RemoveEnemy(_enemy);
            
            _animator.PlayAnimationByName(AnimationStrings.Death);
        }
    }
}