using System.Collections.Generic;
using Code.Infrastructure.GameStateMachineNamespace;
using Code.Infrastructure.GameStateMachineNamespace.States;
using Code.Level;
using Code.Services;
using Code.Services.ChooseLevelService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.ChooseLevelWindow
{
    public class LevelCard : MonoBehaviour, ILocalizable
    {
        [SerializeField] private Button _chooseLevelButton;
        [SerializeField] private Image _levelImage;
        [SerializeField] private TextMeshProUGUI _levelNameText;

        private IGameStateMachine _gameStateMachine;
        private IChooseLevelService _chooseLevelService;
        private LevelType _levelType;
        private string _localizationKey;
        
        public void Init(IGameStateMachine gameStateMachine, IChooseLevelService chooseLevelService,
            LevelType levelType, Sprite levelImage, string localizationKey, bool interactable)
        {
            _gameStateMachine = gameStateMachine;
            _chooseLevelService = chooseLevelService;
            _levelType = levelType;
            _levelImage.sprite = levelImage;
            _localizationKey = localizationKey;
            _chooseLevelButton.interactable = interactable;
        }

        private void OnEnable()
        {
            _chooseLevelButton.onClick.AddListener(OnChooseLevelButtonClicked);
        }

        private void OnDisable()
        {
            _chooseLevelButton.onClick.AddListener(OnChooseLevelButtonClicked);
        }

        private void OnChooseLevelButtonClicked()
        {
            _gameStateMachine.Enter<GameState, LevelType>(_chooseLevelService.ChooseLevel(_levelType));
        }

        public void Localize(Dictionary<string, string> localization)
        {
            _levelNameText.text = localization[_localizationKey];
        }
    }
}