using SportsStore.Web.Data;
using SportsStore.Web.Models;

namespace SportsStore.Web.Interfaces;
public class StoreService(StoreDbContext ctx) : IStoreService
{
    private readonly StoreDbContext _ctx = ctx;

    public IQueryable<Product> Produts => _ctx.Products;
}