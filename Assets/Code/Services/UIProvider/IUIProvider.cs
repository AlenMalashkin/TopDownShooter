using UnityEngine;

namespace Code.Services.UIProvider
{
    public interface IUIProvider : IService
    {
        void ChangeUIRoot(Transform newUIRoot);
        Transform GetRoot();
    }
}