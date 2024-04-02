using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using UnityEngine;

namespace Code.StaticData.EnemyStaticData
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy Config", order = 4)]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private Enemy _prefab;
        public EnemyType Type => _type;
        public Enemy Prefab => _prefab;
    }
}