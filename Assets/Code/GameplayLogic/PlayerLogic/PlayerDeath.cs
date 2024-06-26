using System;
using Code.Services.GameResultService;
using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class PlayerDeath : DeathComponent
    {
        public event Action Death;
        [SerializeField] private AnimatorComponent _animator;

        private IGameFinishService _gameFinishService;

        public void Init(IGameFinishService gameFinishService)
        {
            _gameFinishService = gameFinishService;
        }

        public override void OnDeath(Damageable damageable)
        {
            base.OnDeath(damageable);

            _gameFinishService.FinishGameWithResult(GameResult.Lose);

            if (_animator is PlayerAnimator playerAnimator)
                playerAnimator.PlayDeathAnimation();
            
            Death?.Invoke();
        }
    }
}