using Code.GameplayLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyMovement : EnemyMovement
    {
        [SerializeField] private RangeEnemyAttack _enemyAttack;
        [SerializeField] private NavMeshAgent _agent;
        public bool CanAttack => _canAttack;

        private bool _canAttack;
        private bool _isInShootDistance;
        private float _distanceToPlayer;

        public override void MoveTo(Transform target)
        {
            _isInShootDistance = Vector3.Distance(transform.position, target.position) <= _enemyAttack.ShootDistance;
            
            _canAttack = CheckObstaclesInPlayerDirection(-(transform.position - target.position)) 
                         && _isInShootDistance;
            
            transform.LookAt(target);
            _agent.isStopped = _canAttack;
            _agent.SetDestination(target.position);
        }

        private bool CheckObstaclesInPlayerDirection(Vector3 directon)
        {
            Physics.Raycast(transform.position,
                directon, out RaycastHit hit,
                5f);

            return hit.collider.TryGetComponent(out Player player);
        }
    }
}