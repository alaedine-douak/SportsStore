using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Web.Infrastructure;

namespace SportsStore.UnitTests;

public class PageLinkTagHelperTests
{
    [Fact]
    public void Can_Generate_Page_links()
    {
        var urlHelpers = new Mock<IUrlHelper>();
        urlHelpers.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3");

        var urlHelperFactory = new Mock<IUrlHelperFactory>();
        urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
            .Returns(urlHelpers.Object);

        var viewContext = new Mock<ViewContext>();

        PageLinkTagHelper helper = new(urlHelperFactory.Object) 
        {
            PageModel = new Web.ViewModels.PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            },
            ViewContext = viewContext.Object,
            PageAction = "Test"
        };

        TagHelperContext ctx = new(
            new TagHelperAttributeList(), 
            new Dictionary<object, object>(), "");

        var content = new Mock<TagHelperContent>();
        TagHelperOutput output = new(
            "div", 
            new TagHelperAttributeList(),
            (cache, encode) => Task.FromResult(content.Object));

        
        helper.Process(ctx, output);

        Assert.Equal(@"<a href=""Test/Page1"">1</a>"
            + @"<a href=""Test/Page2"">2</a>"
            + @"<a href=""Test/Page3"">3</a>",
            output.Content.GetContent());
    }
}