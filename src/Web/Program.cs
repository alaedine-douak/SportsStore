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

app.MapDefaultControllerRoute();

app.EnsurePopulatedData();

app.Run();
