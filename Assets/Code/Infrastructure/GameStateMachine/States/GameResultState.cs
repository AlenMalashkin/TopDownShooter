using System;
using Code.Factories.UIFactory;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services.ChooseLevelService;
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
        private IChooseLevelService _chooseLevelService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public GameResultState(IFactoryProvider factoryProvider, IUIProvider uiProvider,
            IChooseLevelService chooseLevelService, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _factoryProvider = factoryProvider;
            _windowFactory = factoryProvider.GetFactory<IWindowFactory>();
            _uiProvider = uiProvider;
            _chooseLevelService = chooseLevelService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter(GameResult payload)
        {
            switch (payload)
            {
                case GameResult.Win:
                    _windowFactory.CreateWinWindow(_uiProvider.GetRoot());
                    _progressService.Progress.LevelsPassed = (int) _chooseLevelService.CurrentLevel;
                    _saveLoadService.SaveProgress();
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