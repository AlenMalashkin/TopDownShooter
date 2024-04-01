using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyDeath : Death
    {
        [SerializeField] private AnimatorComponent _animator;

        private void OnEnable()
        {
            SubscribeOnDeath();
        }

        private void OnDisable()
        {
            UnsubscribeOnDeath();
        }

        public override void OnDeath()
        {
            base.OnDeath();
            
            if (_animator is RangeEnemyAnimator rangeEnemyAnimator)
                rangeEnemyAnimator.PlayDeathAnimation();
        }
    }
}