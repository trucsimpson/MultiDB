using MultiDB.Core.Repositories;
using MultiDB.Infrastructure.Data;

namespace MultiDB.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly IApplicationDbContextFactory _contextFactory;

        public WriteRepository(IApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Add(T entity)
        {
            using var context = _contextFactory.CreateMasterDbContext();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            using var context = _contextFactory.CreateMasterDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateMasterDbContext();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
