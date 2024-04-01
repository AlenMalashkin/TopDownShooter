using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _sightRange = 500f;
        [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;
        
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private Vector3 _walkPoint;
        
        private bool _playerInSightRange;
        private bool _walkPointSet;
        private bool _playerInrightRange;
        
        private float _walkPointRange = 2f;
        private RangeEnemyPlayerDetector _playerDetector;
        private RangeEnemyAnimator _animator;


        public void Init(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Awake()
        {
            _animator = GetComponent<RangeEnemyAnimator>();
            _playerDetector = GetComponent<RangeEnemyPlayerDetector>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer.value);

            if (!_playerInSightRange) Patrolling();
            if (_playerInSightRange) ChasePlayer();

            Debug.Log(_playerInSightRange);
            Debug.Log($"walkpointset {_walkPointSet}");
        }

        private void Patrolling()
        {
            if (!_walkPointSet) SearchWalkPoint();
            
            else
                _agent.SetDestination(_walkPoint);

            Vector3 distanceToWalkPoint = transform.position - _walkPoint;

            _walkPointSet = distanceToWalkPoint.magnitude > 1f;
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
            float randomX = Random.Range(_walkPointRange, -_walkPointRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                _playerTransform.position.z + randomZ);

            _walkPointSet = Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround);
            
        }
        
        private void ChasePlayer()
        {
            _agent.SetDestination(_playerTransform.position);
            transform.LookAt(_playerTransform);

            if (Vector3.Distance(_playerTransform.position, _playerTransform.position) < _sightRange)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
            }
        }
        
    }
}