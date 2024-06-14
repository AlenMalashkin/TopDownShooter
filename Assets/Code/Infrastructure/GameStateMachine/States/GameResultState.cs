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
                    Debug.Log("Win");
                    FinishGameWithWinResult();
                    break;
                case GameResult.Lose:
                    FinishGameWithLoseResult();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(payload), payload, null);
            }
        }

        public void Exit()
        {
        }

        private void FinishGameWithWinResult()
        {
            _windowFactory.CreateWinWindow(_uiProvider.GetRoot());

            if ((int) _chooseLevelService.NextLevel > _progressService.Progress.LevelsPassed)
            {
                _progressService.Progress.LevelsPassed = (int) _chooseLevelService.NextLevel;
                _saveLoadService.SaveProgress();
            }
        }

        private void FinishGameWithLoseResult()
        {
            _windowFactory.CreateLoseWindow(_uiProvider.GetRoot());
        }
    }
}