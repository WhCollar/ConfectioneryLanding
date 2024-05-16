using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.CatalogFeature.Queries;
using ConfectioneryLanding.Features.CatalogFeature.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using ISession = YesSql.ISession;

namespace ConfectioneryLanding.Features.CatalogFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class CatalogController(ISession session, IContentManager contentManager) : Controller
{
    [HttpGet("/api/catalog/filters")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FiltersResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFilters()
    {
        var categories = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Category))
            .ListAsync();

        if (categories == null) return BadRequest();

        List<CategoryResponse> responseCategories = [];
        
        foreach (var category in categories.ToList())
        {
            await contentManager.LoadAsync(category);

            var castedCategory = category.As<Category>();
            responseCategories.Add(new CategoryResponse
            {
                Id = category.ContentItemId,
                Name = castedCategory.Name.Text,
            });
        }
        
        var products = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Product))
            .ListAsync();

        if (products == null) return BadRequest();

        foreach (var product in products.ToList())
        {
            await contentManager.LoadAsync(product);
        }

        var castedProducts = products.Select(product => product.As<Product>()).DefaultIfEmpty();
        var minPrice = castedProducts.Min(product => product.Price.Value) ?? 0;
        var maxPrice = castedProducts.Max(product => product.Price.Value) ?? 0;
        
        return Ok(new FiltersResponse
        {
            Categories = responseCategories,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
        });
    }
    
    [HttpPost("/api/catalog/products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductsResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromBody] GetProductsQuery query)
    {
        var products = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Product))
            .ListAsync();

        if (products == null) return Ok(new List<ProductsResponse>());
        
        foreach (var product in products.ToList())
        {
            await contentManager.LoadAsync(product);
        }

        var castedProducts = products.Select(product => product.As<Product>());
        
        if (query.CategoryIds != null)
        {
            castedProducts = castedProducts.Where(product => 
                product.Categories.ContentItemIds.Any(category => 
                    query.CategoryIds.Any(id => id == category)
                    )
                );
        }
        
        if (query.MinPrice != null)
        {
            castedProducts = castedProducts.Where(product => product.Price.Value >= query.MinPrice);
        }
        
        if (query.MaxPrice != null)
        {
            castedProducts = castedProducts.Where(product => product.Price.Value >= query.MaxPrice);
        }
        
        return Ok(castedProducts.Select(product => new ProductsResponse
        {
            Id = product.ContentItem.ContentItemId,
            Name = product.Name.Text,
            Description = product.Description.Text,
            Categories = product.Categories.ContentItemIds,
            Images = product.Images.Paths,
            Kilocalorie = product.Kilocalorie.Value,
            Weight = product.Weight.Value,
            Width = product.Width.Value,
            Height = product.Height.Value,
            Depth = product.Depth.Value,
            Price = product.Price.Value
        }));
    }
}