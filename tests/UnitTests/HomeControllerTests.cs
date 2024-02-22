using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SportsStore.Web.Controllers;
using SportsStore.Web.Interfaces;
using SportsStore.Web.Models;
using SportsStore.Web.ViewModels;

namespace SportsStore.UnitTests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Use_StoreService()
    {
        Mock<IStoreService> mock = new Mock<IStoreService>();
        mock.Setup(m => m.Produts).Returns((new Product[] {
            new Product { ProductId = 1, Name = "P1"},
            new Product { ProductId = 2, Name = "P2"}
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);

        ProductsListViewModel? result = (controller.Index() as ViewResult)
            ?.ViewData.Model as ProductsListViewModel ?? new();

        Product[] prodArray = result.Products.ToArray(); 
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }

    [Fact]
    public void Can_Paginate()
    {
        Mock<IStoreService> mock = new Mock<IStoreService>();
        mock.Setup(m => m.Produts).Returns((new Product[] {
            new Product { ProductId = 1, Name = "P1"},
            new Product { ProductId = 2, Name = "P2"},
            new Product { ProductId = 3, Name = "P3"},
            new Product { ProductId = 4, Name = "P4"},
            new Product { ProductId = 5, Name = "P5"},
            new Product { ProductId = 6, Name = "P6"},
            new Product { ProductId = 7, Name = "P7"}
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

        ProductsListViewModel result = (controller.Index(2) as ViewResult)
            ?.ViewData.Model as ProductsListViewModel ?? new(); 

        Product[] products = result.Products.ToArray();
        Assert.True(products.Length == 3);
        Assert.Equal("P4", products[0].Name);
        Assert.Equal("P6", products[2].Name);
    }

    [Fact]
    public void Can_Send_Pagination_View_Model()
    {
        // arrange
        Mock<IStoreService> mock = new();
        mock.Setup(x => x.Produts).Returns((new Product[] 
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" },
            new Product { ProductId = 3, Name = "P3" },
            new Product { ProductId = 4, Name = "P4" },
            new Product { ProductId = 5, Name = "P5" },
        }).AsQueryable<Product>());

        HomeController controller = new(mock.Object) { PageSize = 3 };

        // act
        ProductsListViewModel results = (controller.Index(2) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new();

        // assert
        PagingInfo pageInfo = results.PageInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }
}