using System;

namespace Code.Services.SceneLoadService
{
    public interface ISceneLoadService : IService
    {
        void LoadScene(string sceneName, Action onLoad = null);
    }
}