using Code.Data.Progress;

namespace Code.Services.SaveService
{
    public interface IProgressReader
    {
        void ReadProgress(Progress progress);
    }
}