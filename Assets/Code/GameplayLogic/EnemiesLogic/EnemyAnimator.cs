using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class EnemyAnimator : AnimatorComponent
    {
        [SerializeField] private DetectionZone _detectionZone;
        [SerializeField] private Collider _collider;

        private void OnEnable()
        {
            _detectionZone.PlayerDetected += OnPlayerDetected;
            _detectionZone.PlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            _detectionZone.PlayerDetected -= OnPlayerDetected;
            _detectionZone.PlayerLeft -= OnPlayerLeft;
        }
        
        public void PlayDeathAnimation()
            => PlayAnimationByName(AnimationStrings.Death);

        private void OnPlayerDetected()
        {
            SetBool(AnimationStrings.Attack, true);
        }

        private void OnPlayerLeft()
        {
            SetBool(AnimationStrings.Attack, false);
        }

        public void ActivateFist()
        {
            _collider.enabled = true;
        }

        public void DeactivateFist()
        {
            _collider.enabled = false;
        }
    }
}