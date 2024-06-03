using Chat.Core.Constants;
using Chat.Core.Models.Settings;
using Chat.Core.Models.ViewModels;
using Chat.Core.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Chat.Ui.Pages.Chats;

/// <summary>
/// The create chat component.
/// </summary>
public class CreateComponent : ComponentBase
{
    [Inject]
    private IHubService<ChatHubSetting> HubService { get; set; } = default!;

    protected string Name { get; set; } = default!;

    protected IEnumerable<GetChatViewModel> Chats { get; private set; } = default!;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await HubService.StartAsync();
        
        HubService.OnInvoked<GetChatViewModel[]>(HubConstants.Route.GetAllAsync, entities =>
        {
            Chats = entities;
            InvokeAsync(StateHasChanged);
        });

        await HubService.InvokeAsync(HubConstants.Route.GetAllAsync);
    }
    
    protected async Task CreateChat()
    {
        if (!string.IsNullOrEmpty(Name))
        {
            var res = CreateChatViewModel();
            await HubService.InvokeAsync(HubConstants.Route.CreateAsync, res);
        }
    }

    private CreateChatViewModel CreateChatViewModel() => 
        new() { Name = Name };
}