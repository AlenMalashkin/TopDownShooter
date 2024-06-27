using System;
using Code.Audio;
using Code.GameplayLogic;
using Code.Services.EnemiesProvider;
using Code.Tutorial.TutorialWindows;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialEnemy : Enemy
    {
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Collider _hitBox;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _hitSound;

        private IEnemiesProvider _enemiesProvider;
        private DialogWindow _dialogWindow;

        public void Init(IEnemiesProvider enemiesProvider, DialogWindow dialogWindow)
        {
            _enemiesProvider = enemiesProvider;
            _dialogWindow = dialogWindow;
        }
        
        private void OnEnable()
        {
            _damageable.HealthChanged += OnHealthChanged;
            _damageable.Hit += OnHit;
            _damageable.Death += OnDeath;
        }

        private void OnDisable()
        {
            _damageable.HealthChanged -= OnHealthChanged;
            _damageable.Hit -= OnHit;
            _damageable.Death -= OnDeath;
        }

        private void OnHealthChanged(float health)
        {
            _animator.PlayAnimationByName("Impact");
        }

        private void OnDeath(Damageable damageable)
        {
            _hitBox.enabled = false;
            _animator.PlayAnimationByName("Death");
            _enemiesProvider.RemoveEnemy(this);
            _dialogWindow.ShowNextWindow();
            Destroy(gameObject, 3);
        }

        private void OnHit()
        {
            _particleSystem.Play();
            _soundPlayer.PlaySoundEffect(_hitSound);
        }
    }
}