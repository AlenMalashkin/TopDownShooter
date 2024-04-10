using System;
using Code.UI.Windows;

namespace Code.Data
{
    [Serializable]
    public class WindowData
    {
        public WindowType Type;
        public BaseWindow WindowPrefab;
    }
}