using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, ILogger logger)
        {
            try
            {
                // Ensure the database is created and migrations applied
                await context.Database.MigrateAsync();

                logger.LogInformation("✅ Database migrated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ An error occurred while migrating the database.");
                throw;
            }
        }
    }
}




// namespace Ecommerce.Infrastructure.Persistence
// {
//     public static class DbInitializer
//     {
//         public static async Task InitializeAsync(AppDbContext context)
//         {
//             await context.Database.MigrateAsync();
//             await AppDbContextSeed.SeedAsync(context);
//         }
//     }
// }