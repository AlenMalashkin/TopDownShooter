using System.Collections.Generic;
using Code.UI.Windows;
using GamePush;
using TMPro;
using UnityEngine;

namespace Code.Tutorial.TutorialWindows
{
    public class TutorialDialogWindow : DialogWindow, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        
        private string[] _messages;
        private int _currentMessageIndex;
        
        public override void ShowNextWindow()
        {
            if (_currentMessageIndex + 1 > _messages.Length)
                _currentMessageIndex = 0;

            _messageText.text = _messages[_currentMessageIndex];
            _currentMessageIndex += 1;
        }

        public void Localize(Dictionary<string, string> localization)
        {
            if (GP_Device.IsMobile())
            {
                _messages = new[]
                {
                    localization["MessageOneMobile"],
                    localization["MessageTwo"],
                    localization["MessageThree"],
                    localization["MessageFour"]
                };
            }
            else
            {
                _messages = new[]
                {
                    localization["MessageOne"],
                    localization["MessageTwo"],
                    localization["MessageThree"],
                    localization["MessageFour"]
                };
            }
            
            
        }
    }
}