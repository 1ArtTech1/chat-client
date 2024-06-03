using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ChatModel.Ui.Shared;

public class LoginDisplayComponent : ComponentBase
{
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private SignOutSessionStateManager SignOutManager { get; set; } = default!;

    
    protected async Task BeginLogout(MouseEventArgs args)
    {
        const string logoutRequestUri = "authentication/logout";
        
        await SignOutManager.SetSignOutState();
        Navigation.NavigateTo(logoutRequestUri);
    }
}