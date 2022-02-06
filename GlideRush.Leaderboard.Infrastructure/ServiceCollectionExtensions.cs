using System.Reflection;

using GlideRush.Leaderboard.Persistence;
using GlideRush.Leaderboard.Service.CreateLeaderboard;

using MediatR;

using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace GlideRush.Leaderboard.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        private static readonly Assembly serviceAssembly = typeof(CreateLeaderboardResult).Assembly;

        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(serviceAssembly);
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(serviceAssembly);
        }

        public static void ConfigurePersistence(this IServiceCollection services)
        {
            services.AddSingleton<AzureServiceTokenProvider>();
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddDbContext<LeaderboardContext>();
        }
        public static void ConfigureInMemoryPersistence(this IServiceCollection services, bool isTransient = true)
        {
            static LeaderboardContext SqlLiteInMemoryFactory<TContext>(IServiceProvider provider) where TContext : DbContext
            {
                var connection = new SqliteConnection("Filename=:memory:");
                connection.Open();
                var options = new DbContextOptionsBuilder<TContext>()
                    .UseSqlite(connection)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .Options;
                var context = new LeaderboardContext(options, null);
                context.Database.EnsureCreated();
                return context;
            }

            if (isTransient)
            {
                services.AddTransient(SqlLiteInMemoryFactory<LeaderboardContext>);
            }
            else
            {
                services.AddSingleton(SqlLiteInMemoryFactory<LeaderboardContext>);
            }
        }
    }
}