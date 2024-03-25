using Code.GameplayLogic.PlayerLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _rayCastRange = 500f;
        // [SerializeField] private float _
        
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
            if (_playerTransform != null)
            {
                _agent.destination = _playerTransform.position;

                _agent.isStopped = HasNoObstacles(_rayCastRange);

                transform.LookAt(_playerTransform);
                Debug.Log(HasNoObstacles(_rayCastRange));
            }
        }

        private bool HasNoObstacles(float rayCastRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastRange))
            {
                return hit.collider.TryGetComponent(out PlayerMovement playerMovement);
            }

            return false;
        }
        
    }
}