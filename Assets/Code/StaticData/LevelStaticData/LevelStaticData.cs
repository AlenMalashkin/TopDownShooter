using System.Collections.Generic;
using Code.Level;
using UnityEngine;

namespace Code.StaticData.LevelStaticData
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Level Config", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public LevelType Type;
        public string LevelName;
        public List<Vector3> EnemySpawners;
        public Vector3 PlayerPositionOnLevel;
        public bool IsBossLevel;
    }
}