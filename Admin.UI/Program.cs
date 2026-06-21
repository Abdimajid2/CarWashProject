using Admin.UI.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Admin.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        builder.Services.AddHttpClient("Api", (sp, http) =>
        {
            var cfg = sp.GetRequiredService<IConfiguration>();
            var baseUrl = cfg["ApiBaseUrl"];
            http.BaseAddress = new Uri(baseUrl!);
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
            options =>
            {
                //not logged in user sends to
                options.LoginPath = "/login";

                //how long until the cookie expires
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                
                //extend time if user = active
                options.SlidingExpiration = true;
            });

        //active authorize attribute on pages
        builder.Services.AddAuthorization();

        builder.Services.AddCascadingAuthenticationState();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();
       

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
