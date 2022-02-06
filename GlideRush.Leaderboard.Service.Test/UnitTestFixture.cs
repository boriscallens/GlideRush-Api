using GlideRush.Leaderboard.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace GlideRush.Leaderboard.Service.Test
{
    public class UnitTestFixture : IServiceProvider, IDisposable
    {
        private readonly ServiceProvider _provider;

        public UnitTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureMapper();
            serviceCollection.ConfigureInMemoryPersistence();
           _provider = serviceCollection.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _provider.Dispose();
        }
    }
}