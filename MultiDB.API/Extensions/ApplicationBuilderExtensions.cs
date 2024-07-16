using MultiDB.Infrastructure.Data;

namespace MultiDB.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseUpdater = scope.ServiceProvider.GetRequiredService<DatabaseUpdater>();
                databaseUpdater.UpdateAllDatabases().Wait();
            }
            return app;
        }
    }
}
