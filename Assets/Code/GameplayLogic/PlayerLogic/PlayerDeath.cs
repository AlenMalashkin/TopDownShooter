using System.Collections;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerLook))]
    [RequireComponent(typeof(PlayerShoot))]
    [RequireComponent(typeof(Damageable))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy;

        private PlayerAnimator _animator;
        private PlayerMovement _movement;
        private PlayerLook _playerLook;
        private PlayerShoot _playerShoot;
        private Damageable _damageable;

        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();
            _movement = GetComponent<PlayerMovement>();
            _playerLook = GetComponent<PlayerLook>();
            _playerShoot = GetComponent<PlayerShoot>();
            _damageable = GetComponent<Damageable>();
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
            _playerLook.enabled = false;
            _movement.enabled = false;
            _damageable.enabled = false;
            _playerShoot.enabled = false;

            _animator.PlayDeathAnimation();

            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            Destroy(gameObject);
        }
    }
}