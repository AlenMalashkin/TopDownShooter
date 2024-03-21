using Code.Level.Spawners.EnemySpawner;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomPropertyDrawer(typeof(EnemySpawnMarker))]
    public class EnemyMarkerGizmos : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.NonSelected)]
        public static void RenderGizmo(EnemySpawnMarker spawnMarker, GizmoType type)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnMarker.transform.position, 0.5f);
        }
    }
}