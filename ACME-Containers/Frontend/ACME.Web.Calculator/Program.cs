namespace ACME.Web.Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.  
        string? serviceUrl = builder.Configuration["CalculatorService:BaseUrl"];
        if (string.IsNullOrEmpty(serviceUrl))
        {
            throw new InvalidOperationException("CalculatorService:BaseUrl configuration is missing.");
        }
        else
        {
            Console.WriteLine($"CalculatorService BaseUrl: {serviceUrl}");
        }
        builder.Services.AddHttpClient("AddService", client =>
        {
            client.BaseAddress = new Uri(serviceUrl);
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

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Calculator}/{action=Index}");

        app.Run();
    }
}
