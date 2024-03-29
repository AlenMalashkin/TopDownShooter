using Code.Data;
using Code.GameplayLogic;
using Code.Level;
using Code.StaticData.LevelStaticData;
using Code.StaticData.SpawnerStaticData;
using Code.UI.Windows;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        void Load();
        WeaponData ForWeapon(WeaponType type);
        LevelStaticData ForLevel(LevelType type);
        WindowData ForWindow(WindowType type);
        SpawnerStaticData ForSpawner(LevelType type);
    }
}