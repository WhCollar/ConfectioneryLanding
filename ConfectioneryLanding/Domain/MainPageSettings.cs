using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class MainPageSettings : ContentPart
{
    public ContentPickerField CategoryProductSection { get; set; }
    
    public NumericField CategorySectionProductCount { get; set; }
}