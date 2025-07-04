using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

namespace ACME.Web.Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        string? appConfigString = Environment.GetEnvironmentVariable("APPCONFIG");
        if (!string.IsNullOrWhiteSpace(appConfigString) )
        {
            Console.WriteLine("Using APPCONFIG from environment variable.");
            builder.Configuration.AddAzureAppConfiguration(options =>
            {
                string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (string.IsNullOrEmpty(environment))
                {
                    environment = "Development"; // Default to Development if not set
                }
                options.Connect(appConfigString)
                       .Select(KeyFilter.Any, environment)
                       .TrimKeyPrefix("ACME.Web.Calculator:");
                options.UseFeatureFlags(options =>
                {
                    options.Select(KeyFilter.Any, environment);
                    options.SetRefreshInterval(TimeSpan.FromSeconds(30));
                });

            //options.ConfigureKeyVault(cond => {cond.Register})
            });
            builder.Services.AddAzureAppConfiguration();
            builder.Services.AddFeatureManagement();
        }
        else
        {
            Console.WriteLine("No APPCONFIG found in environment variable. Working without Azure AppConfiguration");
        }
        
        string? addUrl = builder.Configuration["CalculatorService:AddUrl"];
        if (string.IsNullOrEmpty(addUrl))
        {
            throw new InvalidOperationException("CalculatorService:AddUrl configuration is missing.");
        }
        else
        {
            Console.WriteLine($"CalculatorService AddUrl: {addUrl}");
        }
        builder.Services.AddHttpClient("AddService", client =>
        {
            client.BaseAddress = new Uri(addUrl);
        });

        string? subtractUrl = builder.Configuration["CalculatorService:SubtractUrl"];
        if (string.IsNullOrEmpty(subtractUrl))
        {
            throw new InvalidOperationException("CalculatorService:SubtractUrl configuration is missing.");
        }
        else
        {
            Console.WriteLine($"CalculatorService SubtractUrl: {subtractUrl}");
        }
        builder.Services.AddHttpClient("SubtractService", client =>
        {
            client.BaseAddress = new Uri(subtractUrl);
        });
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAzureAppConfiguration();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Calculator}/{action=Index}");

        app.Run();
    }
}
