using ConfectioneryLanding.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using ISession = YesSql.ISession;

namespace ConfectioneryLanding.Features.CommentFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class CommentController(ISession session, IContentManager contentManager) : Controller
{
    [HttpGet("/api/comments/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromRoute] int productId)
    {
        Console.WriteLine(productId);
        var comments = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Comment))
            .ListAsync();

        if (comments == null) return BadRequest();
        
        foreach (var comment in comments)
        {
            await contentManager.LoadAsync(comment);
        }

        // Тест необходимо реализовать запланированную логику
        return Ok(string.Join(" ", comments.Select(comment => comment.As<Comment>().FirstName.Text)));
    }
}