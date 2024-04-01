using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.Services;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IEnemyFactory : IService
    {
        Enemy CreateEnemy(Transform followTarget, EnemyType type, Vector3 position);
    }
}