using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{

    public class EnemyDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;

        public override void OnDeath()
        {
            base.OnDeath();
            
            if (_animator is EnemyAnimator enemyAnimator)
                enemyAnimator.PlayDeathAnimation();
        }
    }
}