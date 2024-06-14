namespace Code.Services.PauseService
{
    public interface IPauseService : IService
    {
        bool Paused { get; }
        void Pause();
        void Resume();
    }
}