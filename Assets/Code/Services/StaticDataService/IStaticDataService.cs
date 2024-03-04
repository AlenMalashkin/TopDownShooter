using Code.Services.SceneLoadService;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        void Load();
    }
}