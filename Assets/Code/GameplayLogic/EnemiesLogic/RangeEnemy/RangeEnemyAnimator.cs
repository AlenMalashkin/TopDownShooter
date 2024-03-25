using System;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}