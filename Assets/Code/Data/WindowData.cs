using System;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class WindowData
    {
        public WindowType Type;
        public BaseWindow WindowPrefab;
    }
}