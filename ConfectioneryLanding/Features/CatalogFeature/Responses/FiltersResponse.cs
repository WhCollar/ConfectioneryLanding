namespace ConfectioneryLanding.Features.CatalogFeature.Responses;

public record struct CategoryResponse(string Name, string Id);
public record struct FiltersResponse(List<CategoryResponse> Categories, decimal MinPrice, decimal MaxPrice);