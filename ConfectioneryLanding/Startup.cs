using ConfectioneryLanding.Domain;
using OrchardCore.ContentManagement;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using StartupBase = Microsoft.AspNetCore.Hosting.StartupBase;

namespace ConfectioneryLanding;

[Feature("OrchardCore.ContentTypes")]
public class Startup : StartupBase
{
    public override void Configure(IApplicationBuilder app)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<RequestForm>();
        services.AddContentPart<Category>();
        services.AddContentPart<ContactInfo>();
        services.AddContentPart<Product>();
        services.AddContentPart<Comment>();
        services.AddContentPart<Order>();
        services.AddContentPart<MainPageSettings>();
        services.AddScoped<IDataMigration, Migration>();
    }
}