using Microsoft.EntityFrameworkCore;
using MultiDB.Infrastructure.Configuration;

namespace MultiDB.Infrastructure.Data
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext CreateDbContext(string tenantId);
    }

    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        public ApplicationDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
