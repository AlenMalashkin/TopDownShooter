using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;

        public void Init(Transform playerTransform)
        {
           _playerTransform = playerTransform;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.destination = _playerTransform.position;
        }
    }
}