using UnityEngine;

namespace Code.GameplayLogic.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Vector3 weaponPositionInHand = Vector3.zero;
        [SerializeField] private Quaternion weaponRotationInHand = Quaternion.identity;

        public void AttachToHand(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = weaponPositionInHand;
            transform.localRotation = weaponRotationInHand;
        }
        
    }
}