using System;
using Cinemachine;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Transform _transformToRotate;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            _transformToRotate.LookAt(
                _transformToRotate.position + _camera.transform.rotation * Vector3.forward
                , _camera.transform.rotation * Vector3.up);    
        }
    }
}