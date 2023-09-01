using ClothingStore.DAL;
using ClothingStore.DAL.Repositories;
using ClothingStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClotherStore.Services.Interfaces;
using ClotherStore.Services.Implementions;
using ClothingStore.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");

builder.Services.AddDbContext<StoreAppContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddDbContext<StoreAppContext>(options =>
{
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ClothingStore.DAL.Migrations"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();//esigjsoiehgosdhgoi
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=GetAllProducts}/{id?}");


app.Run();
