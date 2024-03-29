using Code.Level;
using UnityEngine;

namespace Code.StaticData.SpawnerStaticData
{
    [CreateAssetMenu(fileName = "SpawnerStaticData", menuName = "SpawnerStaticData", order = 3)]
    public class SpawnerStaticData : ScriptableObject
    {
        [SerializeField] private LevelType _levelType;
        [SerializeField] private float _spawnTime;
        public LevelType LevelType => _levelType;
        public float SpawnTime => _spawnTime;
    }
}