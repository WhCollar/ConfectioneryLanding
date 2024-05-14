using ConfectioneryLanding.Domain;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Title.Models;

namespace ConfectioneryLanding;

public class Migration(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public int Create()
    {
        DeleteDefaultTypes();
        AddContactInfo();
        AddCategory();
        AddComment();
        AddProduct();

        return 1;
    }

    private void DeleteDefaultTypes()
    {
        contentDefinitionManager.DeletePartDefinition("ContentMenuItem");
        contentDefinitionManager.DeleteTypeDefinition("ContentMenuItem");

        contentDefinitionManager.DeletePartDefinition("HtmlMenuItem");
        contentDefinitionManager.DeleteTypeDefinition("HtmlMenuItem");

        contentDefinitionManager.DeletePartDefinition("LinkMenuItem");
        contentDefinitionManager.DeleteTypeDefinition("LinkMenuItem");

        contentDefinitionManager.DeletePartDefinition("Menu");
        contentDefinitionManager.DeleteTypeDefinition("Menu");
    }

    private void AddContactInfo()
    {
        contentDefinitionManager.AlterTypeDefinition(nameof(ContactInfo), type => type
            .WithPart(nameof(ContactInfo))
            .Stereotype("CustomSettings")
        );

        contentDefinitionManager.AlterPartDefinition(nameof(ContactInfo), part => part
            .WithField(nameof(ContactInfo.Address), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Адресс")
            )
            .WithField(nameof(ContactInfo.Phone), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Телефон")
            )
            .WithField(nameof(ContactInfo.Email), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Электронная почта")
            )
            .WithField(nameof(ContactInfo.FacebookLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Facebook")
            )
            .WithField(nameof(ContactInfo.PinterestLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Pinterest")
            )
            .WithField(nameof(ContactInfo.InstagramLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Instagram")
            )
            .WithField(nameof(ContactInfo.TwitterLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Twitter")
            )
            .WithField(nameof(ContactInfo.LinkedInLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("LinkedIn")
            )
            .WithField(nameof(ContactInfo.TelegramLink), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Telegram")
            )
        );
    }

    private void AddCategory()
    {
        contentDefinitionManager.AlterTypeDefinition(nameof(Category), type => type
            .WithPart(nameof(Category))
            .WithPart(nameof(TitlePart))
            .Creatable()
        );

        contentDefinitionManager.AlterPartDefinition(nameof(Category), part => part
            .WithField(nameof(Category.Name), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Название")
            )
            .WithField(nameof(Category.Desctiption), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Описание")
            )
        );
    }

    private void AddProduct()
    {
        contentDefinitionManager.AlterTypeDefinition(nameof(Product), type => type
            .WithPart(nameof(Product))
            .WithPart(nameof(TitlePart))
            .Creatable()
        );

        contentDefinitionManager.AlterPartDefinition(nameof(Product), part => part
            .WithField(nameof(Product.Name), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Название")
            )
            .WithField(nameof(Product.Desctiption), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Описание")
            )
            .WithField(nameof(Product.Categories), field => field
                .OfType(nameof(ContentPickerField))
                .WithDisplayName("Категории")
                .MergeSettings<ContentPickerFieldSettings>(setting =>
                {
                    setting.DisplayedContentTypes = ["Category"];
                    setting.Multiple = true;
                })
            )
            .WithField(nameof(Product.Kilocalorie), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Киллокалории")
            )
            .WithField(nameof(Product.Weight), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Масса")
            )
            .WithField(nameof(Product.Width), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Ширина")
            )
            .WithField(nameof(Product.Height), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Высота")
            )
            .WithField(nameof(Product.Depth), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Глубина")
            )
            .WithField(nameof(Product.Price), field => field
                .OfType(nameof(NumericField))
                .WithDisplayName("Цена")
            )
        );
    }
    
    private void AddComment()
    {
        contentDefinitionManager.AlterTypeDefinition(nameof(Comment), type => type
            .WithPart(nameof(Comment))
            .WithPart(nameof(TitlePart))
            .Creatable()
        );

        contentDefinitionManager.AlterPartDefinition(nameof(Comment), part => part
            .WithField(nameof(Comment.FirstName), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Фамилия")
            )
            .WithField(nameof(Comment.SecondName), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Имя")
            )
            .WithField(nameof(Comment.Email), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Электронная почта")
            )
            .WithField(nameof(Comment.Product), field => field
                .OfType(nameof(ContentPickerField))
                .WithDisplayName("Продукт")
                .MergeSettings<ContentPickerFieldSettings>(setting =>
                {
                    setting.DisplayedContentTypes = ["Product"];
                    setting.Multiple = false;
                })
            )
            .WithField(nameof(Comment.Text), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Текст")
            )
            .WithField(nameof(Comment.CreatedAt), field => field
                .OfType(nameof(DateField))
                .WithDisplayName("Дата создания")
            )
        );
    }

    private void AddOrder()
    {
        contentDefinitionManager.AlterTypeDefinition(nameof(Order), type => type
            .WithPart(nameof(Order))
            .WithPart(nameof(TitlePart))
            .Creatable()
        );
        contentDefinitionManager.AlterPartDefinition(nameof(Order), part => part
            .WithField(nameof(Order.FirstName), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Фамилия")
            )
            .WithField(nameof(Order.SecondName), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Имя")
            )
            .WithField(nameof(Order.Address), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Адресс")
            )
            .WithField(nameof(Order.Phone), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Телефон")
            )
            .WithField(nameof(Order.Email), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Электронная почта")
            )
            .WithField(nameof(Order.Notes), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Примечание")
            )
            .WithField(nameof(Order.CreatedAt), field => field
                .OfType(nameof(DateField))
                .WithDisplayName("Дата создания")
            )
            .WithField(nameof(Order.Products), field => field
                .OfType(nameof(ContentPickerField))
                .WithDisplayName("Позиции заказа")
                .MergeSettings<ContentPickerFieldSettings>(setting =>
                {
                    setting.DisplayedContentTypes = ["Product"];
                    setting.Multiple = true;
                })
            )
        );
    }
}