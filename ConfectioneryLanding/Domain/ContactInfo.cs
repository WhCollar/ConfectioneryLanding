using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace ConfectioneryLanding.Domain;

public class ContactInfo : ContentPart
{
    public TextField Address { get; set; }

    public TextField Email { get; set; }
    
    public TextField Phone { get; set; }

    public TextField TitleLabel { get; set; }
    
    public TextField Title { get; set; }
    
    public TextField Text { get; set; }
    
    public TextField ContactPageText { get; set; }
    
    public TextField MapLink { get; set; }
    
    public TextField FacebookLink { get; set; }

    public TextField PinterestLink { get; set; }

    public TextField InstagramLink { get; set; }

    public TextField TwitterLink { get; set; }

    public TextField LinkedInLink { get; set; }

    public TextField TelegramLink { get; set; }
}