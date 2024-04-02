using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyAnimator : AnimatorComponent
    {
        private RangeEnemyPlayerDetector _playerDetector;

        private void Awake()
        {
            _playerDetector = GetComponent<RangeEnemyPlayerDetector>();
        }
        
        private void Update()
        {
            SetBool(AnimationStrings.PlayerDetected, _playerDetector.HasNoObstaclesToPlayer());
        }
        
        public void PlayDeathAnimation()
            => PlayAnimationByName(AnimationStrings.Death);
        

        public void PlayRunAnimation()
            => PlayAnimationByName(AnimationStrings.Run);
        
    }
}