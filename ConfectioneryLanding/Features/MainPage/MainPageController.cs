using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Settings;
using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.CatalogFeature.Responses;
using ConfectioneryLanding.Features.MainPage.Responses;
using OrchardCore.ContentManagement.Records;
using YesSql;
using ISession = YesSql.ISession;

namespace ConfectioneryLanding.Features.MainPage;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class MainPageController(ISession session, ISiteService siteService, IContentManager contentManager) : Controller
{
    [HttpGet("/api/main-page-info/category-section")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategorySectionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        var settings = await siteService.GetSiteSettingsAsync();
        var contentItem = settings.As<ContentItem>(nameof(MainPageSettings));
        var mainPageSettings = contentItem.As<MainPageSettings>();

        var categoryId = mainPageSettings.CategoryProductSection.ContentItemIds.FirstOrDefault();

        if (categoryId == null) return BadRequest();
        
        var category = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Category))
            .FirstOrDefaultAsync();

        if (category == null) return NotFound();

        await contentManager.LoadAsync(category);

        var castedCategory = category.As<Category>();
        
        var productCount = mainPageSettings.CategorySectionProductCount.Value ?? 6;
        
        var products = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Product))
            .ListAsync();

        if (products == null) return Ok(new List<ProductsResponse>());
        
        foreach (var product in products.ToList())
        {
            await contentManager.LoadAsync(product);
        }

        var castedProducts = products.Select(product => product.As<Product>());
        
        castedProducts = castedProducts.Where(product => 
            product.Categories.ContentItemIds.Any(category => category == categoryId)
        ).Take(Convert.ToInt32(productCount));
        
        var response = new CategorySectionResponse
        {
            Id = categoryId,
            Name = castedCategory.Name.Text,
            Description = castedCategory.Desctiption.Text,
            Products = castedProducts.Select(product => new ProductsResponse
            {
                Id = product.ContentItem.ContentItemId,
                Name = product.Name.Text,
                Description = product.Description.Text,
                CategoryIds = product.Categories.ContentItemIds,
                Images = product.Images.Paths,
                Kilocalorie = product.Kilocalorie.Value,
                Weight = product.Weight.Value,
                Width = product.Width.Value,
                Height = product.Height.Value,
                Depth = product.Depth.Value,
                Price = product.Price.Value
            }).ToList()
        };

        return Ok(response);
    }
    
    [HttpGet("/api/main-page-info/carousel-items")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CarouselItemResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarouselItem()
    {
        var items = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(CarouselItem))
            .ListAsync();

        if (items == null) return NotFound();

        foreach (var item in items)
        {
            await contentManager.LoadAsync(item);
        }

        var casteditems = items.Select(product => product.As<CarouselItem>());

        var response = casteditems.Select(item => new CarouselItemResponse
        {
            Title = item.Title.Text,
            SecondTitle = item.SecondTitle.Text,
            Description = item.Description.Text,
            Image = item.Image.Paths.FirstOrDefault(),
            ProductId = item.Product.ContentItemIds.FirstOrDefault()
        }).ToList();

        return Ok(response);
    }
}