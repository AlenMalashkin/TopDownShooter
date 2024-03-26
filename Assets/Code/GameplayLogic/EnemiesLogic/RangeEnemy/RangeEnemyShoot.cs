using System;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyShoot : MonoBehaviour
    {
        private RangeEnemyAnimator _animator;
        private RangeEnemyPlayerDetector _playerDetector;

        private void Awake()
        {
            _animator = GetComponent<RangeEnemyAnimator>();
            _playerDetector = GetComponent<RangeEnemyPlayerDetector>();
        }

        private void Update()
        {
            if (_playerDetector.HasNoObstaclesToPlayer())
            {
                Debug.Log(_playerDetector.HasNoObstaclesToPlayer());
            }
        }
    }
}