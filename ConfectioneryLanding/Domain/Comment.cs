using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class Comment : ContentPart
{
    public TextField FirstName { get; set; }

    public TextField SecondName { get; set; }

    public TextField Email { get; set; }
    
    public ContentPickerField Product { get; set; }
    
    public TextField Text { get; set; }
}