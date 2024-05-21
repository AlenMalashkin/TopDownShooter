using Code.Level;
using Code.Utils;

namespace Code.Services.ChooseLevelService
{
    public class ChooseLevelService : IChooseLevelService
    {
        public LevelType CurrentLevel => _levelType;
        public LevelType NextLevel => _levelType.Next();

        private LevelType _levelType;

        public LevelType ChooseLevel(LevelType type)
            => _levelType = type;
    }
}