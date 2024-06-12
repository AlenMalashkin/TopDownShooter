using Code.Data;
using Code.Data.Localization;
using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.GameplayLogic.Weapons;
using Code.Level;
using Code.Pickups;
using Code.StaticData.BossStaticData;
using Code.StaticData.EnemyStaticData;
using Code.StaticData.LevelStaticData;
using Code.StaticData.SpawnerStaticData;
using Code.StaticData.TutorialStaticData;
using Code.UI.Windows;
using GamePush;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        void Load();
        WeaponData ForWeapon(WeaponType type);
        EnemyWeaponData ForEnemyWeapon(EnemyWeaponType type);
        LevelStaticData ForLevel(LevelType type);
        WindowData ForWindow(WindowType type);
        SpawnerStaticData ForSpawner(LevelType type);
        EnemyStaticData ForEnemy(EnemyType type);
        BossStaticData ForBoss(BossType type);
        Pickup ForWeaponPickup(WeaponType type);
        TutorialStaticData ForTutorial();
        Localization ForLocalization(WindowType type);
    }
}