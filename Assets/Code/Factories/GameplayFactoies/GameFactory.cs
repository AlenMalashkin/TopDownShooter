using Code.Services.StaticDataService;

namespace Code.Factories.GameplayFactoies
{
    public class GameFactory : IGameFactory
    {
        private IStaticDataService _staticDataService;

        public GameFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
    }
}