using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _rayCastRange = 500f;
        
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
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
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_playerTransform != null)
            {
                _animator.PlayRunAnimation();
                
                _agent.destination = _playerTransform.position;
                _agent.isStopped = _playerDetector.HasNoObstaclesToPlayer(_rayCastRange);
                
                Debug.Log(_playerDetector.HasNoObstaclesToPlayer());
                transform.LookAt(_playerTransform);

                // transform.LookAt(_playerTransform);
            }
        }
        
        
    }
}