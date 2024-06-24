using Code.Audio;
using Code.Factories.GameplayFactoies;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.Tutorial.TutorialWindows;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialBoss : Enemy
    {
        [SerializeField] private AnimatorComponent _animator;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private Collider _hitBox;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _hitSound;
        
        private IPickupFactory _pickupFactory;
        private DialogWindow _dialogWindow;

        public void Init(IPickupFactory pickupFactory, DialogWindow dialogWindow)
        {
            _pickupFactory = pickupFactory;
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
            _pickupFactory.CrateTutorialPickup(transform.position, WeaponType.Pistol);
            _animator.PlayAnimationByName("Death");
            _dialogWindow.ShowNextWindow();
            Destroy(gameObject, 3);
        }
        
        private void OnHit()
        {
            _particleSystem.Play();
            _soundPlayer.Play(_hitSound);
        }
    }
}