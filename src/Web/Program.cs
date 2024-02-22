using SportsStore.Web.Data;
using SportsStore.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreDbContext>(opts => 
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStoreService, StoreService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("categoryPage",
    "{category}/{productPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page",
    "{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", 
    "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
    "Products/{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();

app.EnsurePopulatedData();

app.Run();
