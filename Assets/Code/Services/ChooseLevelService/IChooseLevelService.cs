using Code.Level;

namespace Code.Services.ChooseLevelService
{
    public interface IChooseLevelService : IService
    {
        LevelType CurrentLevel { get; }
        LevelType NextLevel { get; }
        LevelType ChooseLevel(LevelType type);
    }
}