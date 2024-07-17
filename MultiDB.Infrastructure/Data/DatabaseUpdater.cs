using Microsoft.EntityFrameworkCore;
using MultiDB.Infrastructure.Configuration;

namespace MultiDB.Infrastructure.Data
{
    public class DatabaseUpdater
    {
        private readonly IAppSettings _appSettings;
        private readonly IApplicationDbContextFactory _contextFactory;

        public DatabaseUpdater(IAppSettings appSettings, IApplicationDbContextFactory contextFactory)
        {
            _appSettings = appSettings;
            _contextFactory = contextFactory;
        }

        public async Task UpdateAllDatabases()
        {
            // Update master database
            var masterConnection = _appSettings.GetMasterConnection();
            _contextFactory.CreateDbContext(masterConnection).Database.Migrate();

            // Update replica databases
            var replicaConnections = _appSettings.GetReplicateConnections();
            foreach (var (key, replicateConnection) in replicaConnections)
            {
                _contextFactory.CreateDbContext(replicateConnection).Database.Migrate();
            }
        }
    }
}
