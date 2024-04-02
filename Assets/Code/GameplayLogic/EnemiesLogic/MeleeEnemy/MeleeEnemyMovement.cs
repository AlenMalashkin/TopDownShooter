using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeEnemyMovement : EnemyMovement
    {
        [SerializeField] private NavMeshAgent _agent;
        
        public override void MoveTo(Transform target)
        {
            _agent.SetDestination(target.position);
            transform.LookAt(target);
        }
    }
}