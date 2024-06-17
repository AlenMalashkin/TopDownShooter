using System.Collections.Generic;
using Code.GameplayLogic.EnemiesLogic.Bosses;
using Code.Level;
using UnityEngine;

namespace Code.StaticData.LevelStaticData
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Level Config", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public LevelType Type;
        public string LevelNameTranslationKey;
        public Sprite LevelImage;
        public string LevelPrefabPath;
        public List<Vector3> EnemySpawners;
        public Vector3 PlayerPositionOnLevel;
        public BossType BossType;
    }
}