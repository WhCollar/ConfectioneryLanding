using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Features.CatalogFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class CatalogController(IContentManager contentManager) : Controller
{
    private readonly IContentManager _contentManager = contentManager;
}