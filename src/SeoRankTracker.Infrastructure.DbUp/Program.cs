using System;
using System.IO;
using DbUp;
using Microsoft.Extensions.Configuration;
using SeoRankTracker.Infrastructure.DbUp.Infrastructure;

namespace SeoRankTracker.Infrastructure.DbUp;

internal class Program
{
    static int Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")!;
        var builder = CreateConfigurationBuilder(environment);
        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        DatabaseInitializer.ApplyMigrations(connectionString);
        
        return 0;
    }

    private static IConfigurationBuilder CreateConfigurationBuilder(string environment)
    {
        var path = Directory.GetCurrentDirectory();
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile(environment switch
                {
                    "Docker" => "appsettings.Docker.Development.json",
                    "Development" => "appsettings.Development.json",
                    _ => "appsettings.json"
                },
                optional: false,
                reloadOnChange: true);
    }
}
