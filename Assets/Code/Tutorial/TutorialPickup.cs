using Code.Factories.UIFactory;
using Code.GameplayLogic.PlayerLogic;
using Code.Pickups;
using Code.Services.GameResultService;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialPickup : Pickup
    {
        [SerializeField] private float _rotationSpeed = 25f;
        [SerializeField] private GameObject _rotatableWeapon;

        private IWindowFactory _windowFactory;
        private Transform _root;
        
        public void Init(IWindowFactory windowFactory, Transform root)
        {
            _windowFactory = windowFactory;
            _root = root;
        }
        
        private void Update()
        {
            _rotatableWeapon.transform.Rotate(_rotatableWeapon.transform.up * _rotationSpeed * Time.deltaTime,
                Space.Self);
        }

        public override void PickupItem()
        {
            _windowFactory.CreateTutorialWindow(_root);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                PickupItem();
        }
    }
}