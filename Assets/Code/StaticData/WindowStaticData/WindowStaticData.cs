using Code.Data;
using UnityEngine;

namespace Code.StaticData.WindowStaticData
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Window Config", order = 2)]
    public class WindowStaticData : ScriptableObject
    {
        [SerializeField] private WindowData[] _windows;
        public WindowData[] Windows => _windows;
    }
}