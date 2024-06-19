using Code.Infrastructure.GameStateMachine.States;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.GameResultService;
using Code.UI.HUD;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.Bosses.UniqueBoss
{
    public class FinalBossDeath : DeathComponent
    {
        [SerializeField] private AnimatorComponent _animator;
        
        private HealthBar _healthBar;
        private IGameStateMachine _gameStateMachine;
        
        public void Init(HealthBar healthBar, IGameStateMachine gameStateMachine)
        {
            _healthBar = healthBar;
            _gameStateMachine = gameStateMachine;
        }
        
        public override void OnDeath(Damageable damageable)
        {
            base.OnDeath(damageable);
            
            _animator.PlayAnimationByName(AnimationStrings.Death);
            _gameStateMachine.Enter<GameResultState, GameResult>(GameResult.Win);
            Destroy(_healthBar.gameObject);
        }
    }
}