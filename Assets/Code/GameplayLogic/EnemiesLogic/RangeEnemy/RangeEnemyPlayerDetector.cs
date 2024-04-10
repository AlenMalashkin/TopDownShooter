using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyPlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _shootDistance;
        
        private bool _isInShootDistance;
        private Transform _target;

        public void Init(Transform target)
        {
            _target = target;
        }
        
        public bool CanAttackPlayer()
        {
            _isInShootDistance = Vector3.Distance(transform.position, _target.position) <= _shootDistance;
            return CheckObstaclesInPlayerDirection(-(transform.position - _target.position)) && _isInShootDistance;
        }

        private bool CheckObstaclesInPlayerDirection(Vector3 directon)
        {
            Physics.Raycast(transform.position,
                directon, out RaycastHit hit,
                5f);

            if (hit.collider == null)
                return false;
            
            return hit.collider.TryGetComponent(out Player player);
        }
    }
}