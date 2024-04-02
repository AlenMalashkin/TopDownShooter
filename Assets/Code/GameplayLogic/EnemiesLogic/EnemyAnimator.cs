using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class EnemyAnimator : AnimatorComponent
    {
        [SerializeField] private PlayerDetectionZone playerDetectionZone;

        private void OnEnable()
        {
            playerDetectionZone.PlayerDetected += OnPlayerDetected;
            playerDetectionZone.PlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            playerDetectionZone.PlayerDetected -= OnPlayerDetected;
            playerDetectionZone.PlayerLeft -= OnPlayerLeft;
        }
        
        public void PlayDeathAnimation()
            => PlayAnimationByName(AnimationStrings.Death);

        private void OnPlayerDetected(Collider other)
        {
            SetBool(AnimationStrings.Attack, true);
        }

        private void OnPlayerLeft(Collider other)
        {
            SetBool(AnimationStrings.Attack, false);
        }
    }
}