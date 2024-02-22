using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Interfaces;

namespace SportsStore.Web.Components;
public class NavigationMenuViewComponent(IStoreService storeService) : ViewComponent
{
    private readonly IStoreService _storeService = storeService;

    public IViewComponentResult Invoke() 
    {
        var categories = _storeService.Produts
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);

        ViewBag.SelectedCategory = RouteData?.Values["category"];

        return View(categories);
    }
}