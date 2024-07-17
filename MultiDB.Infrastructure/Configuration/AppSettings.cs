using Microsoft.Extensions.Configuration;

namespace MultiDB.Infrastructure.Configuration
{
    public interface IAppSettings
    {
        string GetMasterConnection();
        string GetReplicateConnection(string tenantId);
        Dictionary<string, string> GetReplicateConnections();
    }

    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetMasterConnection()
        {
            return _configuration.GetConnectionString("MasterDb");
        }

        public string GetReplicateConnection(string tenantId)
        {
            return _configuration.GetConnectionString($"ReplicaDb_{tenantId}");
        }

        public Dictionary<string, string> GetReplicateConnections()
        {
            var replicaConnections = _configuration.GetSection("ConnectionStrings")
                .GetChildren()
                .Where(x => x.Key.StartsWith("ReplicaDb_"))
                .ToDictionary(x => x.Key.Substring("ReplicaDb_".Length), x => x.Value);

            return replicaConnections;
        }
    }
}
