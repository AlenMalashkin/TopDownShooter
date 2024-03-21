using System.Linq;
using Code.Level.Spawners.EnemySpawner;
using Code.Level.Spawners.PlayerSpawner;
using Code.StaticData.LevelStaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            LevelStaticData levelStaticData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelStaticData.LevelName = SceneManager.GetActiveScene().name;
                levelStaticData.EnemySpanwers = FindObjectsOfType<EnemySpawnMarker>()
                    .Select(x => x.transform.position)
                    .ToList();
                levelStaticData.PlayerPositionOnLevel = FindObjectOfType<PlayerSpawnMarker>().transform.position;
            }
			
            EditorUtility.SetDirty(target);
        }
    }
}