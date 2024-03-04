using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GameplayLogic
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject _shootingPoint;

        private float _range = 100f;

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Fire();
            }
        }

        private void Fire()
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