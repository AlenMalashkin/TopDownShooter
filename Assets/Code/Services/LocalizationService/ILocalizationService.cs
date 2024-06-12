using System.Collections.Generic;
using Code.Data.Localization;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Services.LocalizationService
{
    public interface ILocalizationService : IService
    {
        Dictionary<string, string> LoadTranslation(WindowType windowType);
    }
}