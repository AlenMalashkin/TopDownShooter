using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.RangeEnemy;
using Code.GameplayLogic.Weapons;
using Code.Services.StaticDataService;
using Code.StaticData.EnemyStaticData;
using Code.UI.HUD;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class EnemyFactory : IEnemyFactory
    {
        private IStaticDataService _staticDataService;
        private IGameFactory _gameFactory;

        public EnemyFactory(IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
        }

        public Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position)
        {
            return CreateBaseEnemy(EnemyType.Melee, followTarget, position);
        }

        public Enemy CreateRangeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy rangeEnemy = CreateBaseEnemy(EnemyType.Range, followTarget, position);
            RangeEnemyAttack rangeEnemyAttack = rangeEnemy.GetComponent<RangeEnemyAttack>();
            Weapon weapon = _gameFactory.CreateWeapon(WeaponType.Pistol);
            weapon.AttachToHand(rangeEnemyAttack.EnemyArm);
            rangeEnemyAttack.Init(weapon);
            return rangeEnemy;
        }

        private Enemy CreateBaseEnemy(EnemyType type, Transform followTarget, Vector3 position)
        {
            EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
            Enemy enemy = Object.Instantiate(enemyStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(followTarget);
            enemy.GetComponentInChildren<HealthBar>().Init(enemy.GetComponent<Damageable>());
            return enemy;
        }
    }
}