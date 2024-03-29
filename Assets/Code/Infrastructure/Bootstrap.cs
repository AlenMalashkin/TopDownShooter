using Code.Infrastructure.GameStateMachine.States;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services;
using Code.Services.SceneLoadService;
using UnityEngine;

namespace Code.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        private IUpdater _updater;
        
        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Container;
            ISceneLoadService sceneLoadService = new SceneLoadService(this);
            _updater = new Updater();
            IGameStateMachine gameStateMachine = new GameStateMachineNamespace.GameStateMachine(serviceLocator, sceneLoadService, Instantiate(_loadingScreen), _updater);
            gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _updater.Update();
        }
    }
}