using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Interfaces;

namespace SportsStore.Web.Controllers;

public class HomeController(IStoreService storeService) : Controller
{
    private readonly IStoreService _storeService = storeService;

    public int PageSize = 4;

    [HttpGet]
    public IActionResult Index(int productPage = 1)
    {
        var products = _storeService.Produts
            .OrderBy(p => p.ProductId)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize);


        return View(products);
    }
}