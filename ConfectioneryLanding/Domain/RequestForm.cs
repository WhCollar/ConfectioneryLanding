using DocumentFormat.OpenXml.Spreadsheet;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class RequestForm : ContentPart
{
    public TextField FirstName { get; set; }

    public TextField SecondName { get; set; }
    
    public TextField PhoneName { get; set; }

    public TextField Message { get; set; }
}