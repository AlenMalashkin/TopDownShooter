using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;

        public override void OnDeath()
        {
            base.OnDeath();
            
            if (_animator is RangeEnemyAnimator rangeEnemyAnimator)
                rangeEnemyAnimator.PlayDeathAnimation();
        }
    }
}