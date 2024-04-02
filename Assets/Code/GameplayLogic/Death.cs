using UnityEngine;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(AnimatorComponent))]
    [RequireComponent(typeof(Damageable))]
    public abstract class Death : MonoBehaviour
    {
        public abstract void OnDeath();
    }
}