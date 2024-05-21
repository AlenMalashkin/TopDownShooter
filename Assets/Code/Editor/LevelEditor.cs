using System;
using System.Linq;
using Code.Level.Spawners.EnemySpawner;
using Code.Level.Spawners.PlayerSpawner;
using Code.StaticData.LevelStaticData;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

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
                string levelAssetPath =
                    StageUtility.GetCurrentStage().assetPath
                        .Replace("Assets/Resources/", "")
                        .Replace(".prefab", "");
                levelStaticData.LevelPrefabPath = levelAssetPath;
                levelStaticData.EnemySpawners = StageUtility.GetCurrentStageHandle()
                    .FindComponentsOfType<EnemySpawnMarker>()
                    .Select(x => x.transform.position)
                    .ToList();
                levelStaticData.PlayerPositionOnLevel = StageUtility.GetCurrentStageHandle()
                    .FindComponentOfType<PlayerSpawnMarker>().transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}