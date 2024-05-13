using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Features.RequestFormFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class RequestFormController(IContentManager contentManager) : Controller
{
    private readonly IContentManager _contentManager = contentManager;
}