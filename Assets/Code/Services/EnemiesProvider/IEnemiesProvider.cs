using System;
using System.Collections.Generic;
using Code.GameplayLogic;

namespace Code.Services.EnemiesProvider
{
    public interface IEnemiesProvider : IService
    {
        event Action EnemiesChanged;
        List<Enemy> Enemies { get; }
        void AddEnemy(Enemy enemy);
        void RemoveEnemy(Enemy enemy);
    }
}