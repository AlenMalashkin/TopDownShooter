using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.Pickups
{
    public class WeaponPickup : Pickup
    {
        [SerializeField] private float _rotationSpeed = 25f;
        [SerializeField] private GameObject _rotatableWeapon;

        private void Update()
        {
            _rotatableWeapon.transform.Rotate(_rotatableWeapon.transform.up * _rotationSpeed * Time.deltaTime,
                Space.Self);
        }

        public override void PickupItem()
        {
            Destroy(gameObject);
        }
    }
}