using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Services;
using Code.Services.SceneLoadService;
using UnityEngine;

namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Container;
            ISceneLoadService sceneLoadService = new SceneLoadService(this);
            IGameStateMachine gameStateMachine = new GameStateMachineNamespace.GameStateMachine(serviceLocator, sceneLoadService, Instantiate(_loadingScreen));
            gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}