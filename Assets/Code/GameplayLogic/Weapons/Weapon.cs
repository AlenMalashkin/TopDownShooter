using System;
using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {   
        public abstract event Action<int> AmmoChanged;
        public abstract bool CanShoot { get; }
        public abstract bool Reloading { get; }
        public abstract void AttachToHand(Transform parent);
        public abstract void Shoot(Vector3 shootDirection);
        public abstract void ShootBullets(Vector3 direction, int damage);
        public abstract void Reload();

    }
}