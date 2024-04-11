using Code.Services;

namespace Code.Factories.UIFactory
{
    public interface IFactoryProvider : IService
    {
        void AddFactory<TFactory>(IFactory factory) where TFactory : IFactory;
        TFactory GetFactory<TFactory>() where TFactory : IFactory;
    }
}