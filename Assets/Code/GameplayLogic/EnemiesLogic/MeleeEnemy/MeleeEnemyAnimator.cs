namespace Code.GameplayLogic.EnemiesLogic
{
    public class MeleeEnemyAnimator : AnimatorComponent
    {
        public void PlayAttackAnimation()
            => PlayAnimationByName(AnimationStrings.Attack);

        public void PlayRunAnimation()
            => PlayAnimationByName(AnimationStrings.Run);
        
        public void PlayDeathAnimation()
            => PlayAnimationByName(AnimationStrings.Death);
    }
}