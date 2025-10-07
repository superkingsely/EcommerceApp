
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;


// ➡️ This is a generic data seeder, meant for initial dummy data.
// It uses EnsureCreatedAsync() and works fine when you don’t use EF Migrations (for fast demos or prototypes).


namespace ECommerce.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // ✅ Ensure database exists
            await context.Database.EnsureCreatedAsync();

            // ✅ Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Electronics", Description = "Phones, Laptops, Gadgets" },
                    new Category { Name = "Fashion", Description = "Clothes, Shoes, Accessories" },
                    new Category { Name = "Home & Kitchen", Description = "Furniture, Appliances, Utensils" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // ✅ Seed Products
            if (!context.Products.Any())
            {
                var electronics = context.Categories.First(c => c.Name == "Electronics");
                var fashion = context.Categories.First(c => c.Name == "Fashion");

                var products = new List<Product>
                {
                    new Product { Name = "iPhone 15", Description = "Apple smartphone", Price = 1200, CategoryId = electronics.Id },
                    new Product { Name = "Dell XPS 15", Description = "Laptop with Intel i7", Price = 1500, CategoryId = electronics.Id },
                    new Product { Name = "Men’s Sneakers", Description = "Casual shoes", Price = 80, CategoryId = fashion.Id }
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            // ✅ Seed Users
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { FullName = "John Doe", Email = "john@example.com" },
                    new User { FullName = "Jane Smith", Email = "jane@example.com" }
                };
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            // ✅ Create Cart for each User
            if (!context.Carts.Any())
            {
                var users = await context.Users.ToListAsync();
                foreach (var user in users)
                {
                    context.Carts.Add(new Cart { UserId = user.Id });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
