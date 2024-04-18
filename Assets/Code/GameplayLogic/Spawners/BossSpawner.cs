using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.Level;
using Code.Services.EnemiesProvider;
using Code.Services.StaticDataService;
using Code.StaticData.LevelStaticData;
using UnityEngine;

namespace Code.GameplayLogic.Spawners
{
    public class BossSpawner : Spawner
    {
        private EnemySpawner _enemySpawner;
        private IEnemiesProvider _enemiesProvider;
        private IFactoryProvider _factoryProvider;
        private IEnemyFactory _enemyFactory;
        private IStaticDataService _staticDataService;
        private Transform _uiRoot;
        private Transform _followTarget;

        private LevelStaticData _levelStaticData;

        public BossSpawner(EnemySpawner enemySpawner, IEnemiesProvider enemiesProvider,
            IFactoryProvider factoryProvider, IStaticDataService staticDataService, Transform uiRoot,
            Transform followTarget)
        {
            _enemySpawner = enemySpawner;
            _enemiesProvider = enemiesProvider;
            _factoryProvider = factoryProvider;
            _enemyFactory = factoryProvider.GetFactory<IEnemyFactory>();
            _staticDataService = staticDataService;
            _uiRoot = uiRoot;
            _followTarget = followTarget;
        }

        public override void EnableSpawner(Transform target)
        {
            _levelStaticData = _staticDataService.ForLevel(LevelType.Main);

            _enemiesProvider.EnemiesChanged += OnEnemiesChanged;
        }

        public override void DisableSpawner()
        {
            _enemiesProvider.EnemiesChanged -= OnEnemiesChanged;
        }

        private void OnEnemiesChanged()
        {
            Debug.Log(_enemySpawner.EnemiesRemaining);
            Debug.Log(_enemiesProvider.Enemies.Count);
            if (_enemiesProvider.Enemies.Count == 0 && _enemySpawner.EnemiesRemaining <= 0)
                SpawnBoss();
        }

        private void SpawnBoss()
        {
            _enemyFactory.CreateMeleeBoss(_followTarget,
                _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)], _uiRoot);
        }
    }
}