using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(RangeEnemyPlayerDetector))]
    public class RangeEnemyMovement : EnemyMovement
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _sightRange = 500f;
        [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;

        private NavMeshAgent _agent;

        private Vector3 _walkPoint;
        private bool _walkPointSet;
        private bool playerInSightRange;
        private float _walkPointRange;

        private void Awake()
        {
            GetComponent<RangeEnemyAnimator>();
            GetComponent<RangeEnemyPlayerDetector>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);

            if (!playerInSightRange) 
                Patrolling();
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

        public override void MoveTo(Transform target)
        {
            _agent.SetDestination(target.position);
            transform.LookAt(target);
        }
    }
}