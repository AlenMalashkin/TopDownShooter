using Code.GameplayLogic;
using Code.Tutorial.TutorialWindows;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IEnemyFactory : IFactory
    {
        Enemy CreateMeleeEnemy(Transform followTarget, Vector3 position);
        Enemy CreateRangeEnemy(Transform followTarget, Vector3 position);
        Enemy CreateMeleeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot);
        Enemy CreateRangeBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot);
        Enemy CreateUniqueBoss(Transform followTarget, Vector3 position, Transform bossHealthBarRoot);
        Enemy CreateTutorialEnemy(Vector3 position, DialogWindow tutorialDialogWindow);
        Enemy CreateTutorialBoss(Vector3 position, Transform bossHealthBarRoot, DialogWindow dialogWindow);
    }
}