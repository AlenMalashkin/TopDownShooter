namespace Code.Services.GameResultService
{
    public interface IGameFinishService : IService
    {
        void FinishGameWithResult(GameResult gameResult);
    }
}