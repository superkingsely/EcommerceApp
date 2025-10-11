using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Infrastructure.Repositories;

using ECommerce.Application.Interfaces.Services;
using ECommerce.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add controllers, swagger etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// ✅ Register Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    var logger = services.GetRequiredService<ILogger<Program>>();
    await DbInitializer.InitializeAsync(context, logger);

    // await SeedData.SeedAsync(context, services);
}

// ✅ Apply database migrations and seed
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//     await DbInitializer.InitializeAsync(context, logger);
// }

// // ✅ Run migrations and seed data automatically
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     await DbInitializer.InitializeAsync(db);
// }

// ✅ Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
