using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiDB.Infrastructure.Data
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext CreateMasterDbContext();
        ApplicationDbContext CreateReplicaDbContext(string tenantId);
    }

    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext CreateMasterDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MasterDb"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public ApplicationDbContext CreateReplicaDbContext(string tenantId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString($"ReplicaDb_{tenantId}"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
