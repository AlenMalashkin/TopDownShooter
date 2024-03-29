using Code.Factories.GameplayFactoies;
using Code.GameplayLogic.EnemiesLogic;
using Code.Infrastructure;
using Code.Level;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
using Code.StaticData.SpawnerStaticData;
using Code.Utils.Timer;
using UnityEngine;

namespace Code.GameplayLogic.Spawners
{
    public class EnemySpawner : Spawner
    {
        private IUpdater _upater;
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        private ITimer _timer;
        private LevelStaticData _levelStaticData;
        private SpawnerStaticData _spawnerStaticData;
        private Transform _target;

        public EnemySpawner(IUpdater upater, IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _upater = upater;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        public override void EnableSpawner(Transform target)
        {
            _levelStaticData = _staticDataService.ForLevel(LevelType.Main);
            _spawnerStaticData = _staticDataService.ForSpawner(LevelType.Main);
            
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
            _gameFactory.CreateEnemy(
                    _levelStaticData.EnemySpanwers[Random.Range(0, _levelStaticData.EnemySpanwers.Count)])
                .GetComponent<EnemyMovement>()
                .Init(_target);

            _timer.StartTimer(_spawnerStaticData.SpawnTime);
        }
    }
}