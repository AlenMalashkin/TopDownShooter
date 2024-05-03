using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using Code.GameplayLogic.EnemiesLogic.RangeEnemy;
using Code.GameplayLogic.Weapons;
using Code.Services.EnemiesProvider;
using Code.Services.StaticDataService;
using Code.StaticData.BossStaticData;
using Code.StaticData.EnemyStaticData;
using Code.UI.HUD;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class EnemyFactory : IEnemyFactory
    {
        private IStaticDataService _staticDataService;
        private IWeaponFactory _weaponFactory;
        private IHUDFactory _hudFactory;
        private IPickupFactory _pickupFactory;
        private IEnemiesProvider _enemiesProvider;

        public EnemyFactory(IStaticDataService staticDataService, IWeaponFactory weaponFactory, IHUDFactory hudFactory,
            IPickupFactory pickupFactory, IEnemiesProvider enemiesProvider)
        {
            _staticDataService = staticDataService;
            _weaponFactory = weaponFactory;
            _hudFactory = hudFactory;
            _pickupFactory = pickupFactory;
            _enemiesProvider = enemiesProvider;
        }

        public Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy enemy = CreateBaseEnemy(EnemyType.Melee, position);
            enemy.GetComponent<MeleeMovementState>()
                .Init(followTarget);
            enemy.GetComponent<MeleeAttackState>()
                .Init(followTarget);
            enemy.GetComponent<MeleeEnemy>()
                .Init(followTarget.GetComponent<Damageable>());
            enemy.GetComponent<EnemyDeath>()
                .Init(_enemiesProvider);
            return enemy;
        }

        public Enemy CreateRangeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy rangeEnemy = CreateBaseEnemy(EnemyType.Range, position);
            rangeEnemy.GetComponent<RangeMovementState>()
                .Init(followTarget);
            rangeEnemy.GetComponent<RangeEnemyPlayerDetector>()
                .Init(followTarget);
            rangeEnemy.GetComponent<RangeEnemy>()
                .Init(followTarget.GetComponent<Damageable>());
            rangeEnemy.GetComponent<EnemyDeath>()
                .Init(_enemiesProvider);
            RangeAttackState rangeAttackState = rangeEnemy.GetComponent<RangeAttackState>();
            Weapon weapon = _weaponFactory.CreateEnemyWeapon(EnemyWeaponType.RangeEnemyWeapon);
            weapon.AttachToHand(rangeAttackState.EnemyArm);
            rangeAttackState.Init(weapon, followTarget);
            return rangeEnemy;
        }

        public Enemy CreateMeleeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot)
        {
            BossStaticData bossStaticData = _staticDataService.ForBoss(BossType.MeleeBoss);
            Enemy enemy = Object.Instantiate(bossStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponent<MeleeMovementState>().Init(followTarget);
            enemy.GetComponent<MeleeAttackState>().Init(followTarget);
            enemy.GetComponent<MeleeEnemy>().Init(followTarget.GetComponent<Damageable>());
            HealthBar bar = _hudFactory.CreateBossHealthBar(bossHealthBarRoot, enemy.GetComponent<Damageable>());
            enemy.GetComponent<BossDeath>().Init(bar, _pickupFactory);
            return enemy;
        }

        public Enemy CreateRangeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot)
        {
            BossStaticData bossStaticData = _staticDataService.ForBoss(BossType.RangeBoss);
            Enemy rangeBoss = Object.Instantiate(bossStaticData.Prefab, position, Quaternion.identity);
            rangeBoss.GetComponent<RangeMovementState>()
                .Init(followTarget);
            rangeBoss.GetComponent<RangeEnemyPlayerDetector>()
                .Init(followTarget);
            rangeBoss.GetComponent<RangeEnemy>()
                .Init(followTarget.GetComponent<Damageable>());
            RangeAttackState rangeAttackState = rangeBoss.GetComponent<RangeAttackState>();
            Weapon weapon = _weaponFactory.CreateEnemyWeapon(EnemyWeaponType.RangeBossWeapon);
            weapon.AttachToHand(rangeAttackState.EnemyArm);
            rangeAttackState.Init(weapon, followTarget);
            HealthBar bar = _hudFactory.CreateBossHealthBar(bossHealthBarRoot, rangeBoss.GetComponent<Damageable>());
            rangeBoss.GetComponent<BossDeath>().Init(bar, _pickupFactory);
            return rangeBoss;
        }

        public Enemy CreateUniqueBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot)
        {
            BossStaticData bossStaticData = _staticDataService.ForBoss(BossType.UniqueBoss);
            Enemy enemy = Object.Instantiate(bossStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponent<MeleeMovementState>().Init(followTarget);
            enemy.GetComponent<MeleeComboState>().Init(followTarget);
            enemy.GetComponent<UniqueEnemy>().Init(followTarget.GetComponent<Damageable>());
            HealthBar bar = _hudFactory.CreateBossHealthBar(bossHealthBarRoot
                , enemy.GetComponent<Damageable>());
            enemy.GetComponent<BossDeath>().Init(bar, _pickupFactory);
            return enemy;
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