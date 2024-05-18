namespace ConfectioneryLanding.Features.CatalogFeature.Responses;

public record struct ProductsResponse(
 string Id,
 string Name,
 string Description,
 string[] CategoryIds,
 string[] Images, 
 decimal? Kilocalorie,
 decimal? Weight,
 decimal? Width,
 decimal?  Height,
 decimal?  Depth,
 decimal?  Price);