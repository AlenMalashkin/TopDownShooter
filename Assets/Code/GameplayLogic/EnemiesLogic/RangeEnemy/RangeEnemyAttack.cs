
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyAttack : EnemyAttack
    {
        [SerializeField] private float _shootDistance = 5f;

        public float ShootDistance => _shootDistance;
        
        private RangeEnemyAnimator _animator;
        private RangeEnemyPlayerDetector _playerDetector;
        private RangeEnemyMovement _movement;

        private void Awake()
        {
            _animator = GetComponent<RangeEnemyAnimator>();
            _playerDetector = GetComponent<RangeEnemyPlayerDetector>();
            _movement = GetComponent<RangeEnemyMovement>();
        }

        public override void ActivateAttack()
        {
            Debug.Log("Range attack activate test");
        }

        public override void DisableAttack()
        {
            Debug.Log("Range attack disable test");
        }
    }
}