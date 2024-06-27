using System.Collections.Generic;
using Code.Data.Progress;

namespace Code.Services.SaveService
{
    public interface ISaveLoadService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        Progress LoadProgress();
        void SaveProgress();
    }
}