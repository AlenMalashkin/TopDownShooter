using Code.Data.Localization;
using UnityEngine;

namespace Code.StaticData.LocalizationStaticData
{
    [CreateAssetMenu(fileName = "Localization", menuName = "Localization", order = 8)]
    public class LocalizationStaticData : ScriptableObject
    {
        [SerializeField] private Localization[] _localization;
        public Localization[] Localization => _localization;
    }
}