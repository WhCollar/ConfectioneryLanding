namespace ConfectioneryLanding.Features.RequestFormFeature.Commands;

public record struct CreateRequestCommand(
    string FirstName,   
    string SecondName,
    string Phone,
    string Message
);