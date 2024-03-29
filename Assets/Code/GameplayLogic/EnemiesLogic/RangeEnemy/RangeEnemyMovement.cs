using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _sightRange = 500f;
        [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;
        
        private bool _playerInSightRange;
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private Vector3 _walkPoint;
        private bool _walkPointSet;
        private bool playerInSightRange;
        private float _walkPointRange;
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
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);

            if (!playerInSightRange) Patrolling();
            if (playerInSightRange) ChasePlayer();

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
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);

            _walkPointSet = Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround);
            
        }
        
        private void ChasePlayer()
        {
            _agent.SetDestination(_playerTransform.position);
            transform.LookAt(_playerTransform);
        }
        
        
    }
}