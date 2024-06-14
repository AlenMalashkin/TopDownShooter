using Code.Factories.UIFactory;
using Code.Services.PauseService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        
        private IPauseService _pauseService;
        private IWindowFactory _windowFactory;
        private Transform _root;

        public void Init(IPauseService pauseService, IWindowFactory windowFactory, Transform root)
        {
            _pauseService = pauseService;
            _windowFactory = windowFactory;
            _root = root;
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseClicked);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClicked);
        }

        private void OnPauseClicked()
        {
            _windowFactory.CreatePauseWindow(_root);
            _pauseService.Pause();
        }
    }
}