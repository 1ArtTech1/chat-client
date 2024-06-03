using Microsoft.AspNetCore.Components;

namespace ChatModel.Ui.Pages;

public class AuthenticationComponent : ComponentBase
{
    [Parameter]
    public string Action { get; set; } = default!;
}