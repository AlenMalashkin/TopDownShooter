using System;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Vector3 weaponPositionInHand = Vector3.zero;
        [SerializeField] private Quaternion weaponRotationInHand = Quaternion.identity;
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private float _range = 1000f;

        public void AttachToHand(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = weaponPositionInHand;
            transform.localRotation = weaponRotationInHand;
        }

        public void Shoot(Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(_shootingPoint.transform.position, _shootingPoint.transform.forward, out hit, _range))
            {
                Debug.Log(hit.transform.name);
            }   
            else
            {
                Debug.Log("не попал");
            }
        }
    }
}