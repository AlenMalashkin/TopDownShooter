using Code.Level;
using Code.Tutorial;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface ILevelFactory : IFactory
    {
        GameObject CreateLevel(LevelType type);
        TutorialLevel CreateTutorialLevel();
    }
}