using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.Infrastructure;
using Code.Level;
using Code.Services;
using Code.Services.ChooseLevelService;
using Code.Services.EnemiesProvider;
using Code.Services.RandomService;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.StaticData.LevelStaticData;
using Code.StaticData.SpawnerStaticData;
using Code.Utils.Timer;
using UnityEngine;
using Timer = Code.Utils.Timer.Timer;

namespace Code.GameplayLogic.Spawners
{
    public class EnemySpawner : Spawner
    {
        public int EnemiesRemaining => _enemiesRemaining;
        public Transform Target => _target;

        private IUpdater _upater;
        private IFactoryProvider _factoryProvider;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private IEnemyFactory _enemyFactory;
        private IEnemiesProvider _enemiesProvider;
        private IChooseLevelService _chooseLevelService;

        private ITimer _timer;
        private LevelStaticData _levelStaticData;
        private SpawnerStaticData _spawnerStaticData;
        private Transform _target;
        private int _enemiesRemaining;

        public EnemySpawner(IUpdater upater, IFactoryProvider factoryProvider,
            IStaticDataService staticDataService,
            IRandomService randomService, IEnemiesProvider enemiesProvider, IChooseLevelService chooseLevelService)
        {
            _upater = upater;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _factoryProvider = factoryProvider;
            _enemyFactory = factoryProvider.GetFactory<IEnemyFactory>();
            _enemiesProvider = enemiesProvider;
            _chooseLevelService = chooseLevelService;
        }

        public override void EnableSpawner(Transform target)
        {
            _levelStaticData = _staticDataService.ForLevel(_chooseLevelService.CurrentLevel);
            _spawnerStaticData = _staticDataService.ForSpawner(_chooseLevelService.CurrentLevel);
            _enemiesRemaining = _spawnerStaticData.EnemiesOnLevel;

            _target = target;

            _timer = new Timer();
            _timer.TimerFinished += Spawn;

            if (_timer is IUpdateable updateable)
                _upater.Updateables.Add(updateable);
        }

        public override void DisableSpawner()
        {
            if (_timer is IUpdateable updateable)
                _upater.Updateables.Remove(updateable);

            _timer.TimerFinished -= Spawn;
        }

        private void Spawn()
        {
            _enemiesProvider.AddEnemy(SpawnRandomEnemy());

            _enemiesRemaining -= 1;

            if (_enemiesRemaining > 0)
            {
                _timer.StartTimer(_spawnerStaticData.SpawnTime);
            }
            else
            {
                DisableSpawner();
            }
        }

        private Enemy SpawnRandomEnemy()
        {
            if (_randomService.RandomByNumber(0, 100) < 50)
                return _enemyFactory.CreateRangeEnemy(_target,
                    _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)]);

            return _enemyFactory.CreateMeleeEnemy(_target,
                _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)]);
        }
    }
}