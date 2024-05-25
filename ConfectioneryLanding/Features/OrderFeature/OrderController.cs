using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.OrderFeature.Commands;
using ConfectioneryLanding.Features.OrderFeature.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Title.Models;
using YesSql;
using ISession = YesSql.ISession;

namespace ConfectioneryLanding.Features.OrderFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class OrderController(ISession session, IContentManager contentManager) : Controller
{
    [HttpPost("/api/order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var contentItem = await contentManager.NewAsync(nameof(Order));
        
        var part = contentItem.As<Order>();
        part.FirstName = new TextField { Text = command.FirstName };
        part.SecondName = new TextField { Text = command.SecondName };
        part.Address = new TextField { Text = command.Address };
        part.ReceiptType = new ContentPickerField { ContentItemIds = [command.ReceiptTypeId] };
        part.Phone = new TextField { Text = command.Phone };
        part.Email = new TextField { Text = command.Email };
        part.Notes = new TextField { Text = command.Notes };
        part.Products = new ContentPickerField { ContentItemIds = command.ProductIds };
        part.Apply();
        
        await contentManager.CreateAsync(contentItem, VersionOptions.Published);
        
        var titlePart = contentItem.As<TitlePart>();
        titlePart.Title = $"{part.FirstName.Text} {part.SecondName.Text} | {part.Phone.Text}";
        contentItem.DisplayText = titlePart.Title;
        titlePart.Apply();

        await contentManager.UpdateAsync(contentItem);
        
        return new OkResult();
    }

    [HttpPost("/api/receipt-type")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReceiptTypeResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReceiptType()
    {
        var receiptTypes = await session
            .Query<ContentItem, ContentItemIndex>(index => index.ContentType == nameof(ReceiptType))
            .ListAsync();

        if (receiptTypes == null) return BadRequest();

        var contentItems = receiptTypes.ToList();
        
        foreach (var receiptType in contentItems)
        {
            await contentManager.LoadAsync(receiptType);
        }
        
        var receiptTypesResponses = receiptTypes.Select(type => 
            new ReceiptTypeResponse
           {
               Id = type.ContentItemId,
               Name = type.As<TitlePart>().Title,
           });

        return Ok(receiptTypesResponses);
    }
}