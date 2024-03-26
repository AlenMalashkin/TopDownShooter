using System;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.SceneLoadService;
using Code.Services.TimerService;
using UnityEngine;

namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        private ITimerService _timerService; 
        
        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Container;
            ISceneLoadService sceneLoadService = new SceneLoadService(this);
            _timerService = new TimerService();
            IGameStateMachine gameStateMachine = new GameStateMachine(serviceLocator, sceneLoadService, Instantiate(_loadingScreen), _timerService);
            gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _timerService.Tick();
        }
    }
}