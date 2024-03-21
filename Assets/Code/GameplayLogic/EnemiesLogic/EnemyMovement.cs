using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;

        public void Init(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _agent.destination = _playerTransform.position;
            _rigidbody.rotation.SetLookRotation(_playerTransform.position, Vector3.forward);
        }
    }
}