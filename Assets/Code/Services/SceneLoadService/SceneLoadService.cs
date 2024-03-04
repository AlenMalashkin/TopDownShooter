using System;
using System.Collections;
using Code.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.SceneLoadService
{
    public class SceneLoadService : ISceneLoadService
    {
        private ICoroutineRunner _coroutineRunner;

        public SceneLoadService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName, Action onLoad = null)
            => _coroutineRunner.StartCoroutine(LoadSceneAsync(sceneName, onLoad));
        
        private IEnumerator LoadSceneAsync(string sceneName, Action onLoad)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            if (!operation.isDone)
                yield return null;
                    
            onLoad?.Invoke();
        }
    }
}