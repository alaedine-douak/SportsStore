using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Interfaces;
using SportsStore.Web.ViewModels;

namespace SportsStore.Web.Controllers;

public class HomeController(IStoreService storeService) : Controller
{
    private readonly IStoreService _storeService = storeService;

    public int PageSize = 4;

    [HttpGet]
    public IActionResult Index(string? category, int productPage = 1)
    {
        ProductsListViewModel productsVM = new() 
        {
           PageInfo = new PagingInfo
           {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = category == null 
                ? _storeService.Produts.Count()
                : _storeService.Produts.Where(x => x.Category == category).Count()
           },
           Products = _storeService.Produts
            .Where(x => category == null || x.Category == category)
            .OrderBy(x => x.ProductId)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
           CurrentCategory = category
        };

        return View(productsVM);
    }
}