using Code.Tutorial;
using UnityEngine;

namespace Code.StaticData.TutorialStaticData
{
    [CreateAssetMenu(fileName = "TutorialStaticData", menuName = "Tutorial", order = 7)]
    public class TutorialStaticData : ScriptableObject
    {
        [SerializeField] private TutorialLevel _tutorialLevel;
        public TutorialLevel TutorialLevel => _tutorialLevel;
    }
}