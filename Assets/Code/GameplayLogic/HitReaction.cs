using System;
using System.Collections;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class HitReaction : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _componentsToDisalbe;
        [SerializeField] private float _stunTime = 0.5f;

        private float _stunCooldown;
        private float _stunCooldownTimer;

        private Damageable _damageable;
        private AnimatorComponent _animator;


        private void Awake()
        {
            _stunCooldown = _stunTime + 1f;
            _animator = GetComponent<AnimatorComponent>();
            _damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _damageable.Hit -= OnHit;
        }

        private void Update()
        {
            _stunCooldownTimer += _stunCooldown * 1.5f * Time.deltaTime;
            Debug.Log(_stunCooldownTimer > _stunCooldown);
        }

        public void OnHit()
        {   
            // if (_stunCooldownTimer > _stunCooldown)
            {
                for (int i = 0; i < _componentsToDisalbe.Length; i++)
                {
                    _componentsToDisalbe[i].enabled = false;
                }
                
                StartCoroutine(StunRoutine());

                foreach (var component in _componentsToDisalbe)
                    component.enabled = true;
                
                _stunCooldownTimer = 0f;
            }
        }

        private IEnumerator StunRoutine()
        {
            _animator.PlayAnimationByName(AnimationStrings.HitReaction);
            yield return new WaitForSeconds(_stunTime);
        }
    }
}