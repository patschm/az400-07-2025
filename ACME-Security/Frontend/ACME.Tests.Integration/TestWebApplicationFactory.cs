using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;


namespace ACME.Tests.Integration;

public class TestWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddAzureAppConfiguration(options =>
            {
                string? appConfigString = Environment.GetEnvironmentVariable("APPCONFIG");
                if (!string.IsNullOrWhiteSpace(appConfigString))
                {
                    Console.WriteLine("Using APPCONFIG from environment variable.");
                    string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (string.IsNullOrEmpty(environment))
                    {
                        environment = "Development"; // Default to Development if not set
                    }
                    options.Connect(appConfigString)
                           .Select(KeyFilter.Any, environment);
                }
                else
                {
                    Console.WriteLine("No APPCONFIG found in environment variable. Working without Azure AppConfiguration");
                }
            });
        });
    }
 }
