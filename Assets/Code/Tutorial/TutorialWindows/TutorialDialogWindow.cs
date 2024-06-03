using TMPro;
using UnityEngine;

namespace Code.Tutorial.TutorialWindows
{
    public class TutorialDialogWindow : DialogWindow
    {
        [SerializeField] private string[] _messages;
        [SerializeField] private TextMeshProUGUI _messageText;

        private int _currentMessageIndex;
        
        public override void ShowNextWindow()
        {
            if (_currentMessageIndex + 1 > _messages.Length)
                _currentMessageIndex = 0;

            _messageText.text = _messages[_currentMessageIndex];
            _currentMessageIndex += 1;
        }
    }
}