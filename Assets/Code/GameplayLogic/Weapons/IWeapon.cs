using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public interface IWeapon
    {
        void AttachToHand(Transform parent);
    }
}