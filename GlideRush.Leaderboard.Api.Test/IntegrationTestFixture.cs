using GlideRush.Leaderboard.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace GlideRush.Leaderboard.Api.Test
{
    public class IntegrationTestFixture : IServiceProvider, IDisposable
    {
        private readonly ServiceProvider _provider;

        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddConfiguration();
            serviceCollection.ConfigureMapper();
            serviceCollection.ConfigurePersistence();
            serviceCollection.ConfigureMediator();
            serviceCollection.AddLogging();
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