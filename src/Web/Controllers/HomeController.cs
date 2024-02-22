using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Interfaces;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers;

public class HomeController(IStoreService storeService) : Controller
{
    private readonly IStoreService _storeService = storeService;

    public int PageSize = 4;

    [HttpGet]
    public IActionResult Index(int productPage = 1)
    {
        ProductsListViewModel productsVM = new() 
        {
           PageInfo = new PagingInfo
           {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = _storeService.Produts.Count()
           },
           Products = _storeService.Produts
            .OrderBy(x => x.ProductId)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize) 
        };

        return View(productsVM);
    }
}