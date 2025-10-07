using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Linq;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, ILogger logger)
        {
            try
            {
                // Apply pending migrations
                await context.Database.MigrateAsync();

                // ✅ Seed only if no data exists
                if (!context.Users.Any())
                {
                    logger.LogInformation("Seeding initial data...");

                    // --- USERS ---
                    var users = new[]
                    {
                        new User { FullName = "John Doe", Email = "john@example.com", PasswordHash = "hashed-password" },
                        new User { FullName = "Jane     Smith", Email = "jane@example.com", PasswordHash = "hashed-password" }
                    };
                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();

                    // --- CATEGORIES ---
                    var categories = new[]
                    {
                        new Category { Name = "Electronics" },
                        new Category { Name = "Fashion" },
                        new Category { Name = "Books" }
                    };
                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();

                    // --- PRODUCTS ---
                    var products = new[]
                    {
                        new Product { Name = "Smartphone", Description = "Latest Android smartphone", Price = 599.99m, CategoryId = categories[0].Id },
                        new Product { Name = "Headphones", Description = "Wireless noise-cancelling headphones", Price = 199.99m, CategoryId = categories[0].Id },
                        new Product { Name = "T-Shirt", Description = "100% cotton T-shirt", Price = 25.99m, CategoryId = categories[1].Id },
                        new Product { Name = "Novel", Description = "Bestselling fiction novel", Price = 15.49m, CategoryId = categories[2].Id }
                    };
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();

                    logger.LogInformation("✅ Seeding completed successfully.");
                }
                else
                {
                    logger.LogInformation("Database already seeded.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ An error occurred while initializing the database.");
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