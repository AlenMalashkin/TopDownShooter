using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyAnimator : AnimatorComponent
    {
        public void PlayDeathAnimation()
            => PlayAnimationByName(AnimationStrings.Death);
        
        public void PlayRunAnimation()
            => PlayAnimationByName(AnimationStrings.Run);

        public void PlayAttackAnimation()
            => PlayAnimationByName(AnimationStrings.Attack);
    }
}