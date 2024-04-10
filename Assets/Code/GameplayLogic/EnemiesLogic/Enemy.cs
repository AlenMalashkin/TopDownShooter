using UnityEngine;

namespace Code.GameplayLogic
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract void OnEnable();
        public abstract void OnDisable();
        public abstract void Update();
    }
}