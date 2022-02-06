using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GlideRush.Leaderboard.Persistence
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection(string connectionStringName = "Selection");
    }

    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly AzureServiceTokenProvider _azureTokenProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SqlConnectionFactory> _logger;

        public SqlConnectionFactory(AzureServiceTokenProvider azureTokenProvider,
            IConfiguration configuration, ILogger<SqlConnectionFactory> logger)
        {
            _azureTokenProvider = azureTokenProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public SqlConnection CreateConnection(string connectionStringName = "Selection")
        {
            var connectionString = _configuration.GetConnectionString(connectionStringName);
            var connectionInfo = new SqlConnectionStringBuilder(connectionString);

            var localDbOrWithUserId = !string.IsNullOrEmpty(connectionInfo.UserID) ||
                                             connectionInfo.UserInstance ||
                                             connectionInfo.DataSource.ToLower().Contains("(localdb)");
            if (localDbOrWithUserId)
            {
                _logger.LogWarning("Connections string contains user id or it is (localdb) instance, access token will not be used.");
                return new SqlConnection(connectionString);
            }

            return new SqlConnection(connectionString)
            {
                AccessToken = _azureTokenProvider
                    .GetAccessTokenAsync("https://database.windows.net/")
                    .GetAwaiter()
                    .GetResult()
            };
        }
    }
}
