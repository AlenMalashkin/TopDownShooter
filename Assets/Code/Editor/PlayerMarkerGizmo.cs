using Code.Level.Spawners.PlayerSpawner;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(PlayerSpawnMarker))]
    public class PlayerMarkerGizmo : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderGizmo(PlayerSpawnMarker playerSpawnMarker, GizmoType type)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(playerSpawnMarker.transform.position, 0.5f);
        }
    }
}