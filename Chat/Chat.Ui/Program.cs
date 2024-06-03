using Chat.Core.Constants;
using Chat.Core.Services;
using Chat.Core.Services.Interfaces;
using Chat.Ui.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Chat.Ui;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind(AuthenticationConstants.AzureAd, options.ProviderOptions.Authentication);
            options.ProviderOptions.LoginMode = "redirect";
            options.AuthenticationPaths.LogOutSucceededPath = "/logged-out";
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://1testdomainname1.onmicrosoft.com/40a5a276-a75d-4cfe-81c0-0d8343d06b5b/API.Access");
        });

        builder.Services.AddAppSettingsOptions(builder.Configuration);

        builder.Services.AddScoped(typeof(IHubService<>), typeof(HubService<>));
        
        await builder.Build().RunAsync();
    }
}