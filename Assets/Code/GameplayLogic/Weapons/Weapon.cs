using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract bool CanShoot { get; }
        public abstract void AttachToHand(Transform parent);
        public abstract void ShootBullet(Vector3 shootDirection);
        public abstract void Reload();
    }
}