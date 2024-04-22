using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using UnityEngine;

namespace Code.StaticData.BossStaticData
{
    [CreateAssetMenu(fileName = "BossConfig", menuName = "Bosses", order = 6)]
    public class BossStaticData : ScriptableObject
    {
        [SerializeField] private BossType _type;
        [SerializeField] private Enemy _prefab;
        public BossType Type => _type;
        public Enemy Prefab => _prefab;
    }
}