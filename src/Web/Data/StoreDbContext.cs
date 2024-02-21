using SportsStore.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Web.Data;
public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) 
        : base(options) {}

    public DbSet<Product> Products => Set<Product>(); 
}