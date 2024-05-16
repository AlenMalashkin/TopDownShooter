using System;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services.GameResultService;
using Code.Services.InputService;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.UIProvider;
using UnityEngine;

namespace Code.Infrastructure.GameStateMachine.States
{
    public class GameResultState : IPayloadState<GameResult>
    {
        private IFactoryProvider _factoryProvider;
        private IWindowFactory _windowFactory;
        private IUIProvider _uiProvider;
        private IInputService _inputService;
        
        public GameResultState(IFactoryProvider factoryProvider, IUIProvider uiProvider)
        {
            _factoryProvider = factoryProvider;
            _windowFactory = factoryProvider.GetFactory<IWindowFactory>();
            _uiProvider = uiProvider;
        }
        
        public void Enter(GameResult payload)
        {
            switch (payload)
            {
                case GameResult.Win:
                    _windowFactory.CreateWinWindow(_uiProvider.GetRoot());
                    break;
                case GameResult.Lose:
                    _windowFactory.CreateLoseWindow(_uiProvider.GetRoot());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(payload), payload, null);
            }
        }

        public void Exit()
        {
        }
    }
}