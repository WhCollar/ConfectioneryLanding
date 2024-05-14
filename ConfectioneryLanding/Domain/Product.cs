using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class Product : ContentPart
{
    public TextField Name { get; set; }

    public TextField Desctiption { get; set; }

    public ContentPickerField Categories { get; set; }

    public NumericField Kilocalorie { get; set; }
    
    public NumericField Weight { get; set; }

    public NumericField Width { get; set; }

    public NumericField Height { get; set; }

    public NumericField Depth { get; set; }
    
    public NumericField Price { get; set; }
}