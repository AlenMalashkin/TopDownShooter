using UnityEngine;

namespace Code.Services.UIProvider
{
    public class UIProvider : IUIProvider
    {
        private Transform _uiRoot;

        public void ChangeUIRoot(Transform newUIRoot)
            => _uiRoot = newUIRoot;

        public Transform GetRoot()
            => _uiRoot;
    }
}