using UnityEngine;

namespace Code.GameplayLogic.Spawners
{
    public abstract class Spawner
    {
        public abstract void EnableSpawner(Transform target);
        public abstract void DisableSpawner();
    }
}