namespace ConfectioneryLanding.Features.CommentFeature.Responses;

public record struct CommentResponse(
    string ContentItemId,
    string FirstName,
    string SecondName,
    string Email,
    string Text,
    DateTime? CreatedAt);