using System;
using Code.Factories.GameplayFactoies;
using Code.Factories.UIFactory;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.Level;
using Code.Services.EnemiesProvider;
using Code.Services.StaticDataService;
using Code.Services.UIProvider;
using Code.StaticData.LevelStaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.GameplayLogic.Spawners
{
    public class BossSpawner : Spawner
    {
        private EnemySpawner _enemySpawner;
        private IEnemiesProvider _enemiesProvider;
        private IFactoryProvider _factoryProvider;
        private IEnemyFactory _enemyFactory;
        private IStaticDataService _staticDataService;
        private IUIProvider _uiProvider;
        private Transform _followTarget;

        private LevelStaticData _levelStaticData;

        public BossSpawner(EnemySpawner enemySpawner, IEnemiesProvider enemiesProvider,
            IFactoryProvider factoryProvider, IStaticDataService staticDataService, IUIProvider uiProvider)
        {
            _enemySpawner = enemySpawner;
            _enemiesProvider = enemiesProvider;
            _factoryProvider = factoryProvider;
            _enemyFactory = factoryProvider.GetFactory<IEnemyFactory>();
            _staticDataService = staticDataService;
            _uiProvider = uiProvider;
        }

        public override void EnableSpawner(Transform target)
        {
            _levelStaticData = _staticDataService.ForLevel(LevelType.Main);

            _followTarget = target;
            
            _enemiesProvider.EnemiesChanged += OnEnemiesChanged;
        }

        public override void DisableSpawner()
        {
            _enemiesProvider.EnemiesChanged -= OnEnemiesChanged;
        }

        private void OnEnemiesChanged()
        {
            if (_enemiesProvider.Enemies.Count == 0 && _enemySpawner.EnemiesRemaining <= 0)
                SpawnBoss(_levelStaticData.BossType);
        }

        private Enemy SpawnBoss(BossType type)
        {
            switch (type)
            {
                case BossType.MeleeBoss:
                    return _enemyFactory.CreateMeleeBoss(_enemySpawner.Target,
                        _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)], _uiProvider.GetRoot());
                case BossType.RangeBoss:
                    return _enemyFactory.CreateRangeBoss(_enemySpawner.Target,
                        _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)], _uiProvider.GetRoot());
                case BossType.UniqueBoss:
                    return _enemyFactory.CreateUniqueBoss(_enemySpawner.Target,
                        _levelStaticData.EnemySpawners[Random.Range(0, _levelStaticData.EnemySpawners.Count)], _uiProvider.GetRoot());
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}