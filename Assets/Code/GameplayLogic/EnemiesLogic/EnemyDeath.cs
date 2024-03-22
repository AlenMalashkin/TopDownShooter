using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy = 3f;

        private NavMeshAgent _agent;
        private Damageable _damageable;
        private EnemyMovement _movement;
        private EnemyAnimator _animator;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _damageable = GetComponent<Damageable>();
            _animator = GetComponent<EnemyAnimator>();
            _movement = GetComponent<EnemyMovement>();
        }

        private void OnEnable()
        {
            _damageable.Death += OnDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= OnDeath;
        }

        private void OnDeath()
        {
            _animator.PlayDeathAnimation();
            _movement.enabled = false;
            _agent.enabled = false;
            _damageable.enabled = false;

            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            Destroy(gameObject);
        }
    }
}