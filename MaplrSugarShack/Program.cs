using MaplrSugarShack.Services;
using MaplrSugarSnack.Models;
using MaplrSugarSnack.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlite(builder.Configuration.GetConnectionString("MapleSugarShack")));
builder.Services.AddScoped<IReadOnlyRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IReadOnlyRepository<ProductType>, ProductTypeRepository>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp => CartItemRepository.GetCart(sp));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=Products}/{action=Index}/{id?}");

// This code should never be executed on production database
using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.Run();
