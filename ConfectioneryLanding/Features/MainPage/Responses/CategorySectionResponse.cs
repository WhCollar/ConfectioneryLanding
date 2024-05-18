using ConfectioneryLanding.Features.CatalogFeature.Responses;

namespace ConfectioneryLanding.Features.MainPage.Responses;

public record struct CategorySectionResponse(string Id, string Name, string Description, List<ProductsResponse> Products);