using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, ILogger logger)
        {
            try
            {
                // ✅ 1. Apply all pending migrations
                await context.Database.MigrateAsync();
                logger.LogInformation("✅ Database migration applied successfully.");

                // 🧑‍💻 2. Seed USERS (Insert or Update)
                var john = await context.Users.FirstOrDefaultAsync(u => u.Email == "john@example.com");
                if (john == null)
                {
                    // Insert new users
                    var users = new[]
                    {
                        new User { FullName = "Pauljohn Doe", Email = "john@example.com", PasswordHash = "hashed-password" },
                        new User { FullName = "Jane Smith", Email = "jane@example.com", PasswordHash = "hashed-password" }
                    };
                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();
                    logger.LogInformation("👤 Users seeded successfully.");
                }
                else
                {
                    // Update existing user info
                    if (john.FullName != "Paul Doe")
                    {
                        john.FullName = "Paul Doe";
                        await context.SaveChangesAsync();
                        logger.LogInformation("👤 Existing user updated to Paul Doe.");
                    }
                    else
                    {
                        logger.LogInformation("👤 User already up to date.");
                    }
                }

                // 🏷️ 3. Seed CATEGORIES if none exist
                if (!await context.Categories.AnyAsync())
                {
                    var categories = new[]
                    {
                        new Category { Name = "Electronics", Description = "Phones, gadgets & more" },
                        new Category { Name = "Fashion", Description = "Clothing & accessories" },
                        new Category { Name = "Books", Description = "Books & literature" }
                    };
                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                    logger.LogInformation("🏷️ Categories seeded successfully.");
                }
                else
                {
                    logger.LogInformation("🏷️ Categories already exist.");
                }

                // 📦 4. Seed PRODUCTS if none exist
                if (!await context.Products.AnyAsync())
                {
                    var electronics = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Electronics");
                    var fashion = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Fashion");
                    var books = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Books");

                    if (electronics == null || fashion == null || books == null)
                    {
                        logger.LogWarning("⚠️ One or more required categories not found. Product seeding skipped.");
                    }
                    else
                    {
                        var products = new[]
                        {
                            new Product { Name = "Smartphone", Description = "Latest Android smartphone", Price = 599.99m, CategoryId = electronics.Id },
                            new Product { Name = "Headphones", Description = "Wireless noise-cancelling headphones", Price = 199.99m, CategoryId = electronics.Id },
                            new Product { Name = "T-Shirt", Description = "100% cotton T-shirt", Price = 25.99m, CategoryId = fashion.Id },
                            new Product { Name = "Novel", Description = "Bestselling fiction novel", Price = 15.49m, CategoryId = books.Id }
                        };

                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                        logger.LogInformation("📦 Products seeded successfully.");
                    }
                }
                else
                {
                    logger.LogInformation("📦 Products already exist.");
                }

                logger.LogInformation("🎉 Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ An error occurred during database initialization.");
                throw;
            }
        }
    }
}
