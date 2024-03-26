using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.RangeEnemy
{
    public class RangeEnemyPlayerDetector : MonoBehaviour
    {
        
        public bool HasNoObstaclesToPlayer(float rayCastRange = 500f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastRange))
            {
                return hit.collider.TryGetComponent(out PlayerMovement playerMovement);
            }

            return false;
        }
    }
}