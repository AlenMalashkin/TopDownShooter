using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IUIFactory : IFactory
    {
        GameObject CreateRoot();
    }
}
