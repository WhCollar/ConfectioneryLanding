using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace ConfectioneryLanding.Domain;

public class CarouselItem : ContentPart
{
    public TextField Title { get; set; }

    public TextField SecondTitle { get; set; }

    public TextField Description { get; set; }

    public MediaField Image { get; set; }

    public ContentPickerField Product { get; set; }
}