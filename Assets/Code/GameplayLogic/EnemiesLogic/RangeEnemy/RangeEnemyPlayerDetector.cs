using System;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyPlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _shootDistance;
        [SerializeField] private LayerMask _mask;
        
        public event Action PlayerDetected;
        public event Action PlayerLeft;
        
        private bool _isInShootDistance;
        private Transform _target;

        
        
        public void Init(Transform target)
        {
            _target = target;
        }
        
        public void CanAttackPlayer()
        {
            _isInShootDistance = Vector3.Distance(transform.position, _target.position) <= _shootDistance;

            if (CheckObstaclesInPlayerDirection(-(transform.position - _target.position)) && _isInShootDistance)
                PlayerDetected?.Invoke();
            else
                PlayerLeft?.Invoke();
        }

        private bool CheckObstaclesInPlayerDirection(Vector3 direction)
        {
            Physics.Raycast(transform.position,
                direction, out RaycastHit hit,
                5f, _mask);

            if (hit.collider == null)
                return false;

            return hit.collider.TryGetComponent(out Player player);
        }
    }
}