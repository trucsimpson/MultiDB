using Microsoft.EntityFrameworkCore;
using MultiDB.Core.Repositories;
using MultiDB.Infrastructure.Data;

namespace MultiDB.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly IApplicationDbContextFactory _contextFactory;

        public ReadRepository(IApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> GetById(int id, string tenantId)
        {
            using var context = _contextFactory.CreateReplicaDbContext(tenantId);
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll(string tenantId)
        {
            using var context = _contextFactory.CreateReplicaDbContext(tenantId);
            return await context.Set<T>().ToListAsync();
        }
    }
}
