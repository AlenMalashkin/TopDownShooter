using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;

        public override void OnDeath()
        {
            base.OnDeath();
            
            if (_animator is PlayerAnimator playerAnimator)
                playerAnimator.PlayDeathAnimation();
        }
    }
}