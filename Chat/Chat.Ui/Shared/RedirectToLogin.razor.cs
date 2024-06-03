using Microsoft.AspNetCore.Components;

namespace ChatModel.Ui.Shared;

public class RedirectToLoginComponent : ComponentBase
{
    private const string loginRequestUri = "authentication/login?returnUrl={0}";
    
    [Inject]
    private NavigationManager navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        var loginRequestUriFormat = string.Format(loginRequestUri, Uri.EscapeDataString(navigation.Uri));
        
        navigation.NavigateTo(loginRequestUriFormat);
    }
}