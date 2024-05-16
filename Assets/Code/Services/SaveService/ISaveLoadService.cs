using Code.Data.Progress;

namespace Code.Services.SaveService
{
    public interface ISaveLoadService : IService
    {
        Progress LoadProgress();
        void SaveProgress();
    }
}