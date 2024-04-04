using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.Services.StaticDataService;
using Code.StaticData.EnemyStaticData;
using Code.UI.HUD;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class EnemyFactory : IEnemyFactory
    {
        private IStaticDataService _staticDataService;

        public EnemyFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public Enemy CreateEnemy(Transform followTarget, EnemyType type, Vector3 position)
        {
            EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
            Enemy enemy = Object.Instantiate(enemyStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(followTarget);
            
            enemy.GetComponentInChildren<HealthBar>().Init(enemy.GetComponent<Damageable>());
            
            return enemy;
        }
    }
}