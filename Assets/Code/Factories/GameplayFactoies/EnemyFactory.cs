using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
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
        private IWeaponFactory _weaponFactory;

        public EnemyFactory(IStaticDataService staticDataService, IWeaponFactory weaponFactory)
        {
            _staticDataService = staticDataService;
            _weaponFactory = weaponFactory;
        }

        public Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy enemy = CreateBaseEnemy(EnemyType.Melee, position);
            enemy.GetComponent<EnemyMovementState>().Init(followTarget);
            enemy.GetComponent<MeleeAttackState>().Init(followTarget);
            enemy.GetComponent<MeleeEnemy>().Init(followTarget.GetComponent<Damageable>());
            return enemy;
        }

        public Enemy CreateRangeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy rangeEnemy = CreateBaseEnemy(EnemyType.Range, position);
            rangeEnemy.GetComponent<EnemyMovementState>()
                .Init(followTarget);
            rangeEnemy.GetComponent<RangeEnemyPlayerDetector>()
                .Init(followTarget);
            rangeEnemy.GetComponent<RangeEnemy>()
                .Init(followTarget.GetComponent<Damageable>());
            RangeAttackState rangeAttackState = rangeEnemy.GetComponent<RangeAttackState>();
            Weapon weapon = _weaponFactory.CreateWeapon(WeaponType.Pistol);
            weapon.AttachToHand(rangeAttackState.EnemyArm);
            rangeAttackState.Init(weapon, followTarget);
            return rangeEnemy;
        }

        private Enemy CreateBaseEnemy(EnemyType type, Vector3 position)
        {
            EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
            Enemy enemy = Object.Instantiate(enemyStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponentInChildren<HealthBar>().Init(enemy.GetComponent<Damageable>());
            return enemy;
        }
    }
}