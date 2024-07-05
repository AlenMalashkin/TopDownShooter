using System.Collections.Generic;
using Code.GameplayLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.Services.PauseService;
using GamePush;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.RewardWindow
{
    public class RewardWindow : BaseWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _closeButtonText;
        [SerializeField] private TextMeshProUGUI _claimButtonText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _claimButton;
        [SerializeField] private float _rewardHeal = 999f;

        private Damageable _damageable;
        private PlayerDeath _playerDeath;
        private IPauseService _pauseService;

        public void Init(Damageable damageable, PlayerDeath playerDeath, IPauseService pauseService)
        {
            _damageable = damageable;
            _playerDeath = playerDeath;
            _pauseService = pauseService;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _claimButton.onClick.AddListener(OnClaimButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _claimButton.onClick.RemoveListener(OnClaimButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            _playerDeath.OnDeath(_damageable);
            _pauseService.Resume();
            Destroy(gameObject);
        }

        private void OnClaimButtonClicked()
            => GP_Ads.ShowRewarded("RESPAWN", OnRewardAdClaimed, OnRewardedAdStart, OnRewardedAdEnd);

        private void OnRewardAdClaimed(string rewardKey)
        {
            if (rewardKey == "RESPAWN")
            {
                _damageable.Heal(_rewardHeal);
                _pauseService.Resume();
                Destroy(gameObject);
            }
        }

        private void OnRewardedAdStart()
        {
            AudioListener.pause = true;
        }

        private void OnRewardedAdEnd(bool success)
        {
            if (success)
            {
                _pauseService.Resume();
                AudioListener.pause = false;
            }
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _questionText.text = localization["Question"];
            _closeButtonText.text = localization["CloseButtonText"];
            _claimButtonText.text = localization["ClaimButtonText"];
        }
    }
}