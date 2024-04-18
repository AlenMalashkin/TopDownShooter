using Code.Factories.UIFactory;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;

        private IWindowFactory _windowFactory;
        private Transform _uiRoot;
        
        public void Init(IWindowFactory windowFactory, Transform uiRoot)
        {
            _windowFactory = windowFactory;
            _uiRoot = uiRoot;
        }

        public override void OnDeath(Damageable damageable)
        {
            base.OnDeath(damageable);

            _windowFactory.CreateLoseWindow(_uiRoot);
            
            if (_animator is PlayerAnimator playerAnimator)
                playerAnimator.PlayDeathAnimation();
        }
    }
}