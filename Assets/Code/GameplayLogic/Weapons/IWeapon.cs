using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public interface IWeapon
    {
        bool CanShoot { get; }
        void AttachToHand(Transform parent);
        void ShootBullet(Vector3 shootDirection);
        void Reload();
    }
}