using System;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(Animator))]
    public class RangeEnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private RangeEnemyPlayerDetector _playerDetector;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerDetector = GetComponent<RangeEnemyPlayerDetector>();
        }
        
        private void Update()
        {
            _animator.SetBool(AnimationStrings.PlayerDetected, _playerDetector.HasNoObstaclesToPlayer());
        }
        
        public void PlayDeathAnimation()
            => _animator.Play(AnimationStrings.Death);
        

        public void PlayRunAnimation()
            => _animator.Play(AnimationStrings.Run);
        
    }
}