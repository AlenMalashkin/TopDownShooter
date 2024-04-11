using System;
using System.Collections.Generic;
using Code.Factories.UIFactory;

namespace Code.Factories
{
    public class FactoryProvider : IFactoryProvider
    {
        private Dictionary<Type, IFactory> _factoriesMap = new Dictionary<Type, IFactory>();

        public void AddFactory<TFactory>(IFactory factory) where TFactory : IFactory
            => _factoriesMap.Add(typeof(TFactory), factory);
        
        public TFactory GetFactory<TFactory>() where TFactory : IFactory
            => (TFactory) _factoriesMap[typeof(TFactory)];
    }
}