using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyMovement : MonoBehaviour
    {
        public abstract void MoveTo(Transform target);
    }
}