using Code.Factories.UIFactory;
using Code.Services.PauseService;
using Code.Services.UIProvider;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private PlayerDeath _deathComponent;
        [SerializeField] private ParticleSystem _particleSystem;

        private IWindowFactory _windowFactory;
        private IUIProvider _uiProvider;
        private IPauseService _pauseService;
        private bool _rewardClaimed;
        
        public void Init(IWindowFactory windowFactory, IUIProvider uiProvider, IPauseService pauseService)
        {
            _windowFactory = windowFactory;
            _uiProvider = uiProvider;
            _pauseService = pauseService;
        }
        
        private void OnEnable()
        {
            _damageable.Death += OnDeath;
            _damageable.Hit += OnHit;
        }

        private void OnDisable()
        {
            _damageable.Death -= OnDeath;
            _damageable.Hit -= OnHit;
        }

        private void OnDeath(Damageable damageable)
        {
            if (_rewardClaimed)
            {
                _windowFactory.CreateLoseWindow(_uiProvider.GetRoot());
                _deathComponent.OnDeath(damageable);
            }
            else
            {
                _pauseService.Pause();
                _windowFactory.CreateRewardWindow(_uiProvider.GetRoot(), damageable, _deathComponent);
                _rewardClaimed = true;
            }
        }

        private void OnHit()
        {
            _particleSystem.Play();
        }
    }
}