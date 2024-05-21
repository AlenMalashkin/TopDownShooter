using Code.Level;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface ILevelFactory : IFactory
    {
        GameObject CreateLevel(LevelType type);
    }
}