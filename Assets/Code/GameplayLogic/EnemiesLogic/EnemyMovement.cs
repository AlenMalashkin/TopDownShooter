using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_enemy.FollowTarget != null)
            {
                _agent.destination = _enemy.FollowTarget.position;
                _rigidbody.rotation.SetLookRotation(_enemy.FollowTarget.position);
            }
        }
    }
}