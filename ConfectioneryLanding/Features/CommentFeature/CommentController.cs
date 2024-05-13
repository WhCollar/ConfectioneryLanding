using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Features.CommentFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class CommentController(IContentManager contentManager) : Controller
{
    private readonly IContentManager _contentManager = contentManager;
}