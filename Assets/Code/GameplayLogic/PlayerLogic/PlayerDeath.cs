using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerDeath : Death
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
            
            if (_animator is PlayerAnimator playerAnimator)
                playerAnimator.PlayDeathAnimation();
        }
    }
}