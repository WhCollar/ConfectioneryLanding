using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Features.OrderFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class OrderController(IContentManager contentManager) : Controller
{
    private readonly IContentManager _contentManager = contentManager;
}