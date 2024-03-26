using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(RangeEnemyMovement))]
    [RequireComponent(typeof(RangeEnemyAnimator))]
    public class RangeEnemyDeath : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy = 3f;

        private NavMeshAgent _agent;
        private Damageable _damageable;
        private RangeEnemyMovement _movement;
        private RangeEnemyAnimator _animator;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _damageable = GetComponent<Damageable>();
            _animator = GetComponent<RangeEnemyAnimator>();
            _movement = GetComponent<RangeEnemyMovement>();
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