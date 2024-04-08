using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.Services;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IEnemyFactory : IService
    {
        Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position);
        Enemy CreateRangeEnemy(Transform followTarget, Vector3 position);
    }
}