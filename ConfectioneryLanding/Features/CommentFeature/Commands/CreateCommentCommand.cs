namespace ConfectioneryLanding.Features.CommentFeature.Commands;

public record CreateCommentCommand(string FirstName,
    string SecondName,
    string Email,
    string Text);