using Microsoft.EntityFrameworkCore;
using MultiDB.Core.Repositories;
using MultiDB.Infrastructure.Configuration;
using MultiDB.Infrastructure.Data;

namespace MultiDB.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly IAppSettings _appSettings;
        private readonly IApplicationDbContextFactory _contextFactory;

        public ReadRepository(IAppSettings appSettings, IApplicationDbContextFactory contextFactory)
        {
            _appSettings = appSettings;
            _contextFactory = contextFactory;
        }

        public async Task<T> GetById(int id, string tenantId)
        {
            var replicateConnection = _appSettings.GetReplicateConnection(tenantId);
            using var context = _contextFactory.CreateDbContext(replicateConnection);
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll(string tenantId)
        {
            var replicateConnection = _appSettings.GetReplicateConnection(tenantId);
            using var context = _contextFactory.CreateDbContext(replicateConnection);
            return await context.Set<T>().ToListAsync();
        }
    }
}
