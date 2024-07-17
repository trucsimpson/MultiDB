using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MultiDB.Infrastructure.Configuration;

namespace MultiDB.Infrastructure.Data
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// MigrationDbContextFactory implement IDesignTimeDbContextFactory<MigrationDbContext>
        /// Được sử dụng bởi EF Core tools (như khi chạy lệnh Add-Migration hoặc Update-Database từ Package Manager Console)
        /// để tạo DbContext khi thực hiện các thao tác liên quan đến migration.
        /// Nếu không có class này, EF Core tools sẽ không biết cách tạo DbContext khi bạn cố gắng tạo hoặc áp dụng migrations.
        /// </summary>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            IAppSettings appSettings = new AppSettings(configuration);

            var replicaConnections = appSettings.GetReplicateConnections();

            string connectionString = args.Length > 0 && replicaConnections.ContainsKey(args[0])
               ? replicaConnections[args[0]]
               : appSettings.GetMasterConnection();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
