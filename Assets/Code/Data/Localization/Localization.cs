using System;
using System.Collections.Generic;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Data.Localization
{
    [Serializable]
    public class Localization
    {
        public WindowType WindowType;
        public Translation[] Translations;
    }
}