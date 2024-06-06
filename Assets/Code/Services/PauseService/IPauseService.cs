namespace Code.Services.PauseService
{
    public interface IPauseService : IService
    {
        void Pause();
        void Resume();
    }
}