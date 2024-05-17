namespace ConfectioneryLanding.Features.ContactInfoFeature.Responses;

public record struct ContactInfoResponse(
    string Address,
    string Email,
    string Phone,
    string TitleLabel,
    string Title,
    string Text,
    string MapLink,
    string FacebookLink,
    string PinterestLink,
    string InstagramLink,
    string TwitterLink,
    string LinkedInLink,
    string TelegramLink);