using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Web.Components;
using SportsStore.Web.Interfaces;
using SportsStore.Web.Models;

namespace SportsStore.UnitTests;

public class NavigationMenuViewComponentTests
{
    [Fact]
    public void Can_Select_Categories()
    {
        // arrange
        Mock<IStoreService> mock = new();
        mock.Setup(m => m.Produts).Returns((new Product[] 
        {
            new Product { ProductId = 1, Name = "P1", Category = "Apples" },
            new Product { ProductId = 2, Name = "P2", Category = "Apples" },
            new Product { ProductId = 3, Name = "P3", Category = "Plums" },
            new Product { ProductId = 4, Name = "P4", Category = "Oranges" },
        })
        .AsQueryable<Product>());

        NavigationMenuViewComponent target = new(mock.Object);

        // act => get the set of categories
        string[] results = ((IEnumerable<string>?)(target.Invoke() as ViewViewComponentResult)?.ViewData?.Model
            ?? Enumerable.Empty<string>()).ToArray();

        // assert
        Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
    }

    [Fact]
    public void Indicates_Selected_Category()
    {
        // Arrange
        string categoryToSelect = "Apples";

        Mock<IStoreService> mock = new();
        mock.Setup(x => x.Produts).Returns((new Product[]
        {
            new Product { ProductId = 1, Name = "P1", Category = "Apples" },
            new Product { ProductId = 2, Name = "P2", Category = "Oranges" },
        })
        .AsQueryable<Product>());

        NavigationMenuViewComponent target = new(mock.Object);

        target.ViewComponentContext = new ViewComponentContext
        {
            ViewContext = new ViewContext
            {
                RouteData = new RouteData()
            }
        };

        target.RouteData.Values["category"] = categoryToSelect;


        // Action
        string? result = (string?)(target.Invoke() as ViewViewComponentResult)?.ViewData?["selectedCategory"];
    
        // Asset
        Assert.Equal(categoryToSelect, result);
    }
}