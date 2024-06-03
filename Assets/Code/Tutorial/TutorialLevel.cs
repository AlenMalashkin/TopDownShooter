using Code.Level.Spawners.EnemySpawner;
using Code.Level.Spawners.PlayerSpawner;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialLevel : MonoBehaviour
    {
        [SerializeField] private PlayerSpawnMarker _playerSpawnMarker;
        [SerializeField] private EnemySpawnMarker _enemySpawnMarker;
        [SerializeField] private EnemySpawnMarker _bossSpawnMarker;
        [SerializeField] private TutorialTriggerZone _tutorialTriggerZone;
        public PlayerSpawnMarker PlayerSpawnMarker => _playerSpawnMarker;
        public EnemySpawnMarker EnemySpawnMarker => _enemySpawnMarker;
        public EnemySpawnMarker BossSpawnMarker => _bossSpawnMarker;
        public TutorialTriggerZone TutorialTriggerZone => _tutorialTriggerZone;
    }
}