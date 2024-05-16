namespace ConfectioneryLanding.Features.CatalogFeature.Queries;

public record struct GetProductsQuery(string[]? CategoryIds, decimal? MinPrice, decimal? MaxPrice);