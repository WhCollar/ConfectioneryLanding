using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.CommentFeature.Commands;
using ConfectioneryLanding.Features.CommentFeature.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Title.Models;
using YesSql;
using ISession = YesSql.ISession;

namespace ConfectioneryLanding.Features.CommentFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class CommentController(ISession session, IContentManager contentManager) : Controller
{
    [HttpGet("/api/comments/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommentResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByProduct([FromRoute] string productId)
    {
        List<CommentResponse> response = [];
        
        var comments = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(Comment))
            .ListAsync();

        if (comments == null) return BadRequest();

        var contentItems = comments.ToList();
        
        foreach (var comment in contentItems)
        {
            await contentManager.LoadAsync(comment);
        }
        
        foreach (var comment in contentItems)
        {
            var deserializedComment = comment.As<Comment>();
            if (deserializedComment.Product.ContentItemIds.All(x => x != productId)) continue;
            
            var product = await session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentItemId == productId)
                .FirstOrDefaultAsync();

            response.Add(new ()
            {
                ContentItemId = comment.ContentItemId,
                FirstName = deserializedComment.FirstName.Text,
                SecondName =  deserializedComment.SecondName.Text,
                Email= deserializedComment.Email.Text,
                Text= deserializedComment.Text.Text,
                CreatedAt = comment.CreatedUtc,
            });
        }

        return Ok(response);
    }

    [HttpPost("/api/comments/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment([FromRoute] string productId, [FromBody] CreateCommentCommand command)
    {
        var contentItem = await contentManager.NewAsync(nameof(Comment));

        var part = contentItem.As<Comment>();
        part.FirstName = new TextField { Text = command.FirstName };
        part.SecondName = new TextField { Text = command.SecondName };
        part.Email = new TextField { Text = command.Email };
        part.Text = new TextField { Text = command.Text };
        part.Product = new ContentPickerField { ContentItemIds = [productId] };

        await contentManager.CreateAsync(contentItem, VersionOptions.Published);
        
        var titlePart = contentItem.As<TitlePart>();
        titlePart.Title = $"{part.FirstName} {part.SecondName} | {part.Email}";
        contentItem.DisplayText = titlePart.Title;
        titlePart.Apply();

        await contentManager.UpdateAsync(contentItem);

        var response = new CommentResponse
        {
            ContentItemId = contentItem.ContentItemId,
            FirstName = part.FirstName.Text,
            SecondName = part.SecondName.Text,
            Email = part.Email.Text,
            Text = part.Text.Text,
            CreatedAt = contentItem.CreatedUtc,
        };
        
        return Ok(response);
    }
}