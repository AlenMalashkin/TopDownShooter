using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public abstract class AIStateMachineBase : MonoBehaviour
    {
        public abstract void EnterState<T>() where T : AIState;
        public abstract void UpdateState();
    }
}