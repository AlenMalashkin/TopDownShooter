using Code.GameplayLogic.PlayerLogic;
using Code.Services.GameResultService;
using UnityEngine;

namespace Code.Pickups
{
    public class WeaponPickup : Pickup
    {
        [SerializeField] private float _rotationSpeed = 25f;
        [SerializeField] private GameObject _rotatableWeapon;

        private IGameFinishService _gameFinishService;
        
        public void Init(IGameFinishService gameFinishService)
        {
            _gameFinishService = gameFinishService;
        }
        
        private void Update()
        {
            _rotatableWeapon.transform.Rotate(_rotatableWeapon.transform.up * _rotationSpeed * Time.deltaTime,
                Space.Self);
        }

        public override void PickupItem()
        {
            _gameFinishService.FinishGameWithResult(GameResult.Win);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                PickupItem();
        }
    }
}