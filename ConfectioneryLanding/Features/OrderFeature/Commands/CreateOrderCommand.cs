namespace ConfectioneryLanding.Features.OrderFeature.Commands;

public record struct CreateOrderCommand(
    string FirstName,
    string SecondName,
    string ReceiptTypeId,
    string Address,
    string Phone,
    string Email,
    string Notes,
    string[] ProductIds);