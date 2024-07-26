using Microsoft.Extensions.DependencyInjection;
using SeoRankTracker.Application.Services;
using SeoRankTracker.Infrastructure.Services;

namespace SeoRankTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGoogleScrapingService, GoogleScrapingService>();
        
        serviceCollection.AddHttpClient(Constants.GoogleScraperHttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://www.google.com/");
            
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            // TODO: Implement decoding and uncomment
            // client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            
            client.Timeout = TimeSpan.FromSeconds(10);
        });
        return serviceCollection;
    }
}