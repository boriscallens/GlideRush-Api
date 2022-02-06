namespace GlideRush.Leaderboard.Domain.Exceptions
{
    public class ConfigurationException: Exception
    {
        public string ConfigurationPath { get; }

        public ConfigurationException(string message, string configurationPath, Exception innerException) : base(message, innerException)
        {
            ConfigurationPath = configurationPath;
        }
    }
}