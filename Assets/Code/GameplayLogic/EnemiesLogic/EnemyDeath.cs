using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{

    public class EnemyDeath : Death
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
            
            if (_animator is EnemyAnimator enemyAnimator)
                enemyAnimator.PlayDeathAnimation();
        }
    }
}