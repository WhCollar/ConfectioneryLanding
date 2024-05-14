using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class Order : ContentPart
{
    public TextField FirstName { get; set; }

    public TextField SecondName { get; set; }
    
    public TextField Address { get; set; }

    public TextField Phone { get; set; }
    
    public TextField Email { get; set; }

    public TextField Notes { get; set; }

    public DateField CreatedAt { get; set; }
    
    public ContentPickerField Products { get; set; }
}