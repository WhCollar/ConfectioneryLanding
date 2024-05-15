using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.RequestFormFeature.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;

namespace ConfectioneryLanding.Features.RequestFormFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class RequestFormController(IContentManager contentManager) : Controller
{
    [HttpPost("/api/request-form")]
    public async Task<IActionResult> Add([FromBody] CreateRequestCommand command)
    {
        var contentItem = await contentManager.NewAsync(nameof(RequestForm));

        var part = contentItem.As<RequestForm>();
        part.FirstName = new() { Text = command.FirstName };
        part.SecondName = new() { Text = command.FirstName };
        part.Phone = new() { Text = command.Phone };
        part.Message = new() { Text = command.Message };

        await contentManager.CreateAsync(contentItem, VersionOptions.Published);
        
        var titlePart = contentItem.As<TitlePart>();
        titlePart.Title = $"{part.FirstName.Text} {part.SecondName.Text} | {part.Phone.Text}";
        contentItem.DisplayText = titlePart.Title;
        titlePart.Apply();

        await contentManager.UpdateAsync(contentItem);

        return Ok();
    }
}