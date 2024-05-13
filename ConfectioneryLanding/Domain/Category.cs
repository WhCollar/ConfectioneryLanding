using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class Category : ContentPart
{
    public TextField Name { get; set; }
    
    public TextField Desctiption { get; set; }
}