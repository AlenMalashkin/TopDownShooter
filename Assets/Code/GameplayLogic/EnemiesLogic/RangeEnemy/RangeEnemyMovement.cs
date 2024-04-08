using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyMovement : EnemyMovement
    {
        [SerializeField] private NavMeshAgent _agent;

        public override void MoveTo(Transform target)
        {
            transform.LookAt(target);
            _agent.SetDestination(target.position);
        }
    }
}