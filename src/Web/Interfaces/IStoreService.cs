using SportsStore.Web.Models;

namespace SportsStore.Web.Interfaces;
public interface IStoreService
{
    IQueryable<Product> Produts { get; }
}