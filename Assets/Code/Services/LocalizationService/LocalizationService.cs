using System;
using System.Collections.Generic;
using Code.Data.Localization;
using Code.Services.StaticDataService;
using Code.UI.Windows;
using GamePush;

namespace Code.Services.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private IStaticDataService _staticDataService;
        
        public LocalizationService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public Dictionary<string, string> LoadTranslation(WindowType windowType)
            => Translate(GP_Language.Current(), _staticDataService.ForLocalization(windowType).Translations);

        private Dictionary<string, string> Translate(Language language, Translation[] translations)
        {
            Dictionary<string, string> neededTranslations = new Dictionary<string, string>();

            foreach (var translation in translations)
            {
                switch (language)
                {
                    case Language.English:
                        neededTranslations.Add(translation.Key, translation.EnglishTranslation);
                        break;
                    case Language.Russian:
                        neededTranslations.Add(translation.Key, translation.RussianTranslation);
                        break;
                    default:
                        neededTranslations.Add(translation.Key, translation.EnglishTranslation);
                        break;
                }
            }  
            
            return neededTranslations;
        }
    }
}