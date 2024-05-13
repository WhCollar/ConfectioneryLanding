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
        services.AddContentPart<Category>();
        services.AddContentPart<Comment>();
        services.AddContentPart<ContactInfo>();
        services.AddContentPart<Product>();
        services.AddScoped<IDataMigration, Migration>();
    }
}