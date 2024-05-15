using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.RequestFormFeature.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;

namespace ConfectioneryLanding.Features.RequestFormFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class RequestFormController(IContentManager contentManager) : Controller
{
    [HttpPost("/api/request-form")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CreateRequestCommand command)
    {
        var contentItem = await contentManager.NewAsync(nameof(RequestForm));

        var part = contentItem.As<RequestForm>();
        part.FirstName = new TextField { Text = command.FirstName };
        part.SecondName = new TextField { Text = command.SecondName };
        part.Phone = new TextField { Text = command.Phone };
        part.Message = new TextField { Text = command.Message };
        part.Apply();
        
        await contentManager.CreateAsync(contentItem, VersionOptions.Published);
        
        var titlePart = contentItem.As<TitlePart>();
        titlePart.Title = $"{part.FirstName.Text} {part.SecondName.Text} | {part.Phone.Text}";
        contentItem.DisplayText = titlePart.Title;
        titlePart.Apply();

        await contentManager.UpdateAsync(contentItem);

        return Ok();
    }
}