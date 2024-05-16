using Code.Data.Progress;

namespace Code.Services.ProgressService
{
    public interface IProgressService : IService
    {
        Progress Progress { get; set; }
    }
}