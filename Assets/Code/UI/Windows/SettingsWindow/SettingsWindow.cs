using System.Collections.Generic;
using Code.Services.ProgressService;
using Code.Services.SaveService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.SettingsWindow
{
    public class SettingsWindow : BaseWindow, ILocalizable
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _saveSettingsButton;
        [SerializeField] private TextMeshProUGUI _settingsText;
        [SerializeField] private TextMeshProUGUI _effectsSettingsText;
        [SerializeField] private TextMeshProUGUI _musicSettingsText;
        [SerializeField] private TextMeshProUGUI _saveSettingsText;
        [SerializeField] private Slider _effectVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        public void Init(IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            _effectVolumeSlider.value = _progressService.Progress.Settings.EffectVolume;
            _musicVolumeSlider.value = _progressService.Progress.Settings.MusicVolume;
        }

        private void OnEnable()
        {
            _effectVolumeSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _saveSettingsButton.onClick.AddListener(OnSaveSettingsClicked);
        }

        private void OnDisable()
        {
            _effectVolumeSlider.onValueChanged.RemoveListener(OnEffectVolumeChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _saveSettingsButton.onClick.RemoveListener(OnSaveSettingsClicked);
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _settingsText.text = localization["SettingsText"];
            _musicSettingsText.text = localization["MusicVolume"];
            _effectsSettingsText.text = localization["EffectsVolume"];
            _saveSettingsText.text = localization["SaveSettings"];
        }

        private void OnEffectVolumeChanged(float value)
            => _progressService.Progress.Settings.EffectVolume = value;

        private void OnMusicVolumeChanged(float value)
            => _progressService.Progress.Settings.MusicVolume = value;

        private void OnCloseButtonClicked()
            => Destroy(gameObject);

        private void OnSaveSettingsClicked()
            => _saveLoadService.SaveProgress();
    }
}