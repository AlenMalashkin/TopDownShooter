namespace Code.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Container => _instance ??= new ServiceLocator();
        private static ServiceLocator _instance;

        public void RegisterService<TService>(TService service) where TService : IService
            => ServiceInstance<TService>.Service = service;

        public TService Resolve<TService>() where TService : IService
            => ServiceInstance<TService>.Service;

        private class ServiceInstance<TService> where TService : IService
        {
            public static TService Service;
        }
    }
}