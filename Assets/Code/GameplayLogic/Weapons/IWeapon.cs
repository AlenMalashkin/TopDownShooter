using UnityEngine;

namespace Code.GameplayLogic
{
    public interface IWeapon
    {
        void AttachToHand(Transform parent);
        void Shoot(Vector3 direction);
    }
}