using SportsStore.Web.Models;

namespace SportsStore.Web.ViewModels;

public class ProductsListViewModel
{
    public PagingInfo PageInfo { get; set; } = new();
    public IEnumerable<Product> Products { get; set; }  = Enumerable.Empty<Product>();
}