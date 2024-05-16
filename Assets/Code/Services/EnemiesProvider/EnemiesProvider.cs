using System;
using System.Collections.Generic;
using Code.GameplayLogic;

namespace Code.Services.EnemiesProvider
{
    public class EnemiesProvider : IEnemiesProvider
    {
        List<Enemy> _enemies = new List<Enemy>();

        public event Action<int> EnemiesChanged;
        public List<Enemy> Enemies => _enemies;

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            EnemiesChanged?.Invoke(_enemies.Count);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            EnemiesChanged?.Invoke(_enemies.Count);
        }
    }
}