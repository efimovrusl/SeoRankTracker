using Microsoft.Extensions.DependencyInjection;
using SeoRankTracker.Application.Services;

namespace SeoRankTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWebsiteRankService, WebsiteRankService>();
        return serviceCollection;
    }
}