using Code.Audio;
using Code.Factories.UIFactory;
using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.EnemiesLogic.Bosses.UniqueBoss;
using Code.GameplayLogic.EnemiesLogic.MeleeEnemy;
using Code.GameplayLogic.EnemiesLogic.RangeEnemy;
using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Weapons;
using Code.Infrastructure;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Services.EnemiesProvider;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;
using Code.StaticData.BossStaticData;
using Code.StaticData.EnemyStaticData;
using Code.Tutorial;
using Code.Tutorial.TutorialWindows;
using Code.UI.HUD;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public class EnemyFactory : IEnemyFactory
    {
        private IGameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;
        private IWeaponFactory _weaponFactory;
        private IHUDFactory _hudFactory;
        private IPickupFactory _pickupFactory;
        private IEnemiesProvider _enemiesProvider;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IUpdater _updater;

        public EnemyFactory(IGameStateMachine gameStateMachine, IStaticDataService staticDataService, IWeaponFactory weaponFactory,
            IHUDFactory hudFactory, IPickupFactory pickupFactory, IEnemiesProvider enemiesProvider,
            IProgressService progressService, ISaveLoadService saveLoadService,IUpdater updater)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _weaponFactory = weaponFactory;
            _hudFactory = hudFactory;
            _pickupFactory = pickupFactory;
            _enemiesProvider = enemiesProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _updater = updater;
        }

        public Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position)
        {
            Enemy enemy = CreateBaseEnemy(EnemyType.Melee, position);
            enemy.GetComponent<EnemyMovementState>()
                .Init(followTarget);
            enemy.GetComponent<BaseMeleeAttackState>()
                .Init(followTarget);
            enemy.GetComponent<MeleeEnemy>()
                .Init(followTarget.GetComponent<PlayerDeath>());
            enemy.GetComponent<EnemyDeath>()
                .Init(_enemiesProvider);
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
                .Init(followTarget.GetComponent<PlayerDeath>());
            rangeEnemy.GetComponent<EnemyDeath>()
                .Init(_enemiesProvider);
            RangeAttackState rangeAttackState = rangeEnemy.GetComponent<RangeAttackState>();
            Weapon weapon = _weaponFactory.CreateEnemyWeapon(EnemyWeaponType.RangeEnemyWeapon);
            weapon.AttachToHand(rangeAttackState.EnemyArm);
            rangeAttackState.Init(weapon, followTarget);
            return rangeEnemy;
        }

        public Enemy CreateTutorialEnemy(Vector3 position, DialogWindow tutorialDialogWindow)
        {
            Enemy enemy = CreateBaseEnemy(EnemyType.TutorialEnemy, position);
            enemy.GetComponent<TutorialEnemy>().Init(_enemiesProvider, tutorialDialogWindow);
            enemy.GetComponentInChildren<HealthBar>().Init(enemy.GetComponent<Damageable>());
            return enemy;
        }

        public Enemy CreateMeleeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot)
        {
            Enemy enemy = CreateBaseBoss(BossType.MeleeBoss, position);
            enemy.GetComponent<EnemyMovementState>()
                .Init(followTarget);
            enemy.GetComponent<BaseMeleeAttackState>()
                .Init(followTarget);
            enemy.GetComponent<MeleeEnemy>()
                .Init(followTarget.GetComponent<PlayerDeath>());
            HealthBar bar = _hudFactory.CreateBossHealthBar(bossHealthBarRoot, enemy.GetComponent<Damageable>());
            enemy.GetComponent<BossDeath>().Init(bar, _pickupFactory);
            return enemy;
        }

        public Enemy CreateRangeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot)
        {
            Enemy rangeBoss = CreateBaseBoss(BossType.RangeBoss, position);
            rangeBoss.GetComponent<EnemyMovementState>()
                .Init(followTarget);
            rangeBoss.GetComponent<RangeEnemyPlayerDetector>()
                .Init(followTarget);
            rangeBoss.GetComponent<RangeEnemy>()
                .Init(followTarget.GetComponent<PlayerDeath>());
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
            Enemy enemy = CreateBaseBoss(BossType.UniqueBoss, position);
            enemy.GetComponent<FinalBossMovementState>().Init(followTarget);
            enemy.GetComponent<MeleeComboState>().Init(followTarget, _updater);
            enemy.GetComponent<FinalBoss>().Init(followTarget.GetComponent<PlayerDeath>());
            HealthBar bar = _hudFactory.CreateBossHealthBar(bossHealthBarRoot
                , enemy.GetComponent<Damageable>());
            enemy.GetComponent<FinalBossDeath>().Init(bar, _gameStateMachine);
            return enemy;
        }

        public Enemy CreateTutorialBoss(Vector3 position, Transform bossHealthBarRoot, DialogWindow dialogWindow)
        {
            Enemy boss = CreateBaseBoss(BossType.TutorialBoss, position);
            boss.GetComponent<TutorialBoss>().Init(_pickupFactory, dialogWindow);
            _hudFactory.CreateBossHealthBar(bossHealthBarRoot, boss.GetComponent<Damageable>());
            return boss;
        }

        private Enemy CreateBaseEnemy(EnemyType type, Vector3 position)
        {
            EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(type);
            Enemy enemy = Object.Instantiate(enemyStaticData.Prefab, position, Quaternion.identity);
            enemy.GetComponentInChildren<HealthBar>().Init(enemy.GetComponent<Damageable>());
            enemy.GetComponent<SoundPlayer>().Init(_progressService, _saveLoadService);
            return enemy;
        }

        private Enemy CreateBaseBoss(BossType type, Vector3 position)
        {
            BossStaticData bossStaticData = _staticDataService.ForBoss(type);
            Enemy boss = Object.Instantiate(bossStaticData.Prefab, position, Quaternion.identity);
            boss.GetComponent<SoundPlayer>().Init(_progressService, _saveLoadService);
            return boss;
        }
    }
}