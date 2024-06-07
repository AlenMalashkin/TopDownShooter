using Code.GameplayLogic.PlayerLogic;
using Code.GameplayLogic.Weapons;
using Code.Services.GameResultService;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Pickups
{
    public class WeaponPickup : Pickup
    {
        [SerializeField] private float _rotationSpeed = 25f;
        [SerializeField] private GameObject _rotatableWeapon;
        [SerializeField] private WeaponType _weaponType;

        private IGameFinishService _gameFinishService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public void Init(IGameFinishService gameFinishService,
            IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameFinishService = gameFinishService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Update()
        {
            _rotatableWeapon.transform.Rotate(_rotatableWeapon.transform.up * _rotationSpeed * Time.deltaTime,
                Space.Self);
        }

        public override void PickupItem()
        {
            _gameFinishService.FinishGameWithResult(GameResult.Win);
            _progressService.Progress.CollectedWeapons.Add(_weaponType);
            _saveLoadService.SaveProgress();
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                PickupItem();
        }
    }
}