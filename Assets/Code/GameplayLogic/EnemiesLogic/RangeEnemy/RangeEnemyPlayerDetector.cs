using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyPlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _shootDistance;
        
        private bool _isInShootDistance;
        
        public bool CanAttackPlayer(Transform target)
        {
            _isInShootDistance = Vector3.Distance(transform.position, target.position) <= _shootDistance;
            return CheckObstaclesInPlayerDirection(-(transform.position - target.position)) && _isInShootDistance;
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