using Chat.Core.Constants;
using Chat.Core.Models.Settings;
using Chat.Core.Models.ViewModels;
using Chat.Core.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Chat.Ui.Pages.Chats;

/// <summary>
/// The send message component.
/// </summary>
public class SendMessageComponent : ComponentBase
{
    protected int ChatId { get; set; }

    protected int MessageChatId { get; set; }

    protected string Value { get; set; } = default!;

    [Inject]
    private IHubService<ChatHubSetting> ChatHubService { get; set; } = default!;
    
    [Inject]
    private IHubService<MessageHubSetting> MessageHubService { get; set; } = default!;
    
    protected IEnumerable<GetChatViewModel> Chats { get; private set; } = default!;

    protected IEnumerable<GetMessageViewModel> Messages { get; private set; } = default!;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await ChatHubService.StartAsync();
        await MessageHubService.StartAsync();
        
        ChatHubService.OnInvoked<GetChatViewModel[]>(HubConstants.Route.GetAllAsync, entities =>
        {
            Chats = entities;
            InvokeAsync(StateHasChanged);
        });
        
        MessageHubService.OnInvoked<GetMessageViewModel[]>(HubConstants.Route.GetByChatIdAsync, entities =>
        {
            Messages = entities;
            InvokeAsync(StateHasChanged);
        });

        await ChatHubService.InvokeAsync(HubConstants.Route.GetAllAsync);
    }
    
    protected async Task SendMessageAsync()
    {
        if (!string.IsNullOrEmpty(Value))
        {
            var createMessage = CreateMessageViewModel();
            await MessageHubService.InvokeAsync(HubConstants.Route.JoinGroup, ChatId);
            await MessageHubService.InvokeAsync(HubConstants.Route.CreateAsync, createMessage);
        }
    }

    protected async Task GetMessagesByChatId()
    {
        await MessageHubService.InvokeAsync(HubConstants.Route.JoinGroup, MessageChatId);
        await MessageHubService.InvokeAsync(HubConstants.Route.GetByChatIdAsync, MessageChatId);
    }

    private CreateMessageViewModel CreateMessageViewModel() => 
        new() { ChatId = ChatId, Value = Value, DateTime = DateTime.Now };
}