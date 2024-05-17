using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Settings;
using ConfectioneryLanding.Domain;
using ConfectioneryLanding.Features.ContactInfoFeature.Responses;
namespace ConfectioneryLanding.Features.ContactInfoFeature;

[IgnoreAntiforgeryToken, AllowAnonymous]
public class ContactInfoController(ISiteService siteService) : Controller
{
    [HttpGet("/api/contact-info")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactInfoResponse))]
    public async Task<IActionResult> Init()
    {
        var settings = await siteService.GetSiteSettingsAsync();
        var contentItem = settings.As<ContentItem>(nameof(ContactInfo));
        var contactInfo = contentItem.As<ContactInfo>();

        var response = new ContactInfoResponse(
            Address: contactInfo.Address.Text,
            Email: contactInfo.Email.Text,
            Phone: contactInfo.Phone.Text,
            TitleLabel: contactInfo.TitleLabel.Text,
            Title: contactInfo.Title.Text,
            Text: contactInfo.Text.Text,
            MapLink: contactInfo.MapLink.Text,
            FacebookLink: contactInfo.FacebookLink.Text,
            PinterestLink: contactInfo.PinterestLink.Text,
            InstagramLink: contactInfo.InstagramLink.Text,
            TwitterLink: contactInfo.LinkedInLink.Text,
            LinkedInLink: contactInfo.LinkedInLink.Text,
            TelegramLink: contactInfo.TelegramLink.Text
        );

        return Ok(response);
    }
}