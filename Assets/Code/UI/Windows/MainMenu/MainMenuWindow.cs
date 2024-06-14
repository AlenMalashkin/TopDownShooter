using System.Collections.Generic;
using Code.UI.Windows.MainMenu.Buttons;
using UnityEngine;

namespace Code.UI.Windows.MainMenu
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private EquipmentButton _equipmentButton;
        [SerializeField] private ChooseLevelButton _chooseLevelButton;
        public EquipmentButton EquipmentButton => _equipmentButton;
        public ChooseLevelButton ChooseLevelButton => _chooseLevelButton;
        
        public void Init(Dictionary<string, string> translations)
        {
            _chooseLevelButton.SetButtonText(translations["PlayButtonText"]);
            _equipmentButton.SetButtonText(translations["ShopButtonText"]);
        }
    }
}