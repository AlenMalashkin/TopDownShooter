using System;
using Code.Factories.UIFactory;
using Code.Level;
using UnityEngine;

namespace Code.UI.Windows.ChooseLevelWindow
{
    public class ChooseLevelWindow : BaseWindow
    {
        [SerializeField] private Transform _levelCardRoot;

        private IUIFactory _uiFactory;
        
        public void Init(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        private void Start()
        {
            foreach (var levelType in (LevelType[])Enum.GetValues(typeof(LevelType)))
            {
                _uiFactory.CreateLevelCard(levelType, _levelCardRoot);
            }
        }
    }
}