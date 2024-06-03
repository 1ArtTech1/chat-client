using Microsoft.AspNetCore.Components;

namespace ChatModel.Ui.Shared;

public class NavMenuComponent : ComponentBase
{
    private string className = "collapse";
    private bool isMenuOpen = true;

    protected string NavMenuCssClass => HandleClassName();

    protected void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
    }
    
    private string HandleClassName() =>
        isMenuOpen ? className : string.Empty;
}