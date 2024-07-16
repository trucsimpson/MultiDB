using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiDB.Infrastructure.Data
{
    public class DatabaseUpdater
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContextFactory _contextFactory;

        public DatabaseUpdater(IConfiguration configuration, IApplicationDbContextFactory contextFactory)
        {
            _configuration = configuration;
            _contextFactory = contextFactory;
        }

        public async Task UpdateAllDatabases()
        {
            // Update master database
            await UpdateDatabase(_contextFactory.CreateMasterDbContext());

            // Update replica databases
            var replicaConnections = _configuration.GetSection("ConnectionStrings")
                .GetChildren()
                .Where(x => x.Key.StartsWith("ReplicaDb_Tenant"))
                .ToDictionary(x => x.Key, x => x.Value);
            foreach (var (key, connectionString) in replicaConnections)
            {
                var tenantId = key.Substring("ReplicaDb_".Length);
                await UpdateDatabase(_contextFactory.CreateReplicaDbContext(tenantId));
            }
        }

        private async Task UpdateDatabase(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
        }
    }
}
