using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public abstract class AIState : MonoBehaviour
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
    }
}