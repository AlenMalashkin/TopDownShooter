using UnityEngine;

namespace Code.GameplayLogic
{
    public abstract class Death : MonoBehaviour
    {
        public abstract void OnDeath(Damageable damageable);
    }
}