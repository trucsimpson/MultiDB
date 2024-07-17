using Microsoft.EntityFrameworkCore;
using MultiDB.Core.Repositories;
using MultiDB.Infrastructure.Configuration;
using MultiDB.Infrastructure.Data;

namespace MultiDB.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly IAppSettings _appSettings;
        private readonly IApplicationDbContextFactory _contextFactory;

        public WriteRepository(IAppSettings appSettings, IApplicationDbContextFactory contextFactory)
        {
            _appSettings = appSettings;
            _contextFactory = contextFactory;
        }

        public Task Add(T entity) => ExecuteDbOperation(async context =>
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        });

        public Task Update(T entity) => ExecuteDbOperation(async context =>
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        });

        public Task Delete(int id) => ExecuteDbOperation(async context =>
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        });

        private async Task ExecuteDbOperation(Func<DbContext, Task> operation)
        {
            var masterConnection = _appSettings.GetMasterConnection();
            using var context = _contextFactory.CreateDbContext(masterConnection);
            await operation(context);
        }
    }
}
