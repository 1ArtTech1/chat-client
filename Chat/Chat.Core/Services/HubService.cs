using Chat.Core.Models.Settings;
using Chat.Core.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Chat.Core.Services;

/// <inheritdoc />
public class HubService<TSetting> : IHubService<TSetting>
    where TSetting : BaseSetting 
{
    private HubConnection hubConnection;
    private readonly IAccessTokenProvider accessTokenProvider;

    /// <summary>
    /// Initializes a new instance of the <see><cref>HubService</cref></see>class.
    /// </summary>
    /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> a instance.</param>
    /// <param name="option">The <see cref="IOptionsMonitor{TSetting}"/> a instance.</param>
    public HubService(
        IAccessTokenProvider accessTokenProvider,
        IOptionsMonitor<TSetting> option)
    {
        this.accessTokenProvider = accessTokenProvider;
        hubConnection = HubConnectionBuilder(option);
    }
    
    /// <inheritdoc />
    public async Task StartAsync()
    {
        if (hubConnection.State is HubConnectionState.Disconnected)
        {
            await hubConnection.StartAsync();
        }
    }
    
    /// <inheritdoc />
    public async Task StopAsync()
    {
        if (hubConnection.State is not HubConnectionState.Disconnected)
        {
            await hubConnection.StopAsync();
        }
    }

    /// <inheritdoc />
    public async Task InvokeAsync<TEntity>(string name, TEntity entity)
    {
        await hubConnection.InvokeAsync(name, entity);
    }
    
    /// <inheritdoc />
    public async Task InvokeAsync(string name)
    {
        await hubConnection.InvokeAsync(name);
    }
    
    /// <inheritdoc />
    public void OnInvoked<TEntity>(string name, Action<TEntity> handler)
    {
        hubConnection.On(name, handler);
    }

    private HubConnection HubConnectionBuilder(IOptionsMonitor<TSetting> option)
    {
        var requestUri = option.CurrentValue.Url + option.CurrentValue.Route;
        
        return hubConnection = new HubConnectionBuilder()
            .WithUrl(
                requestUri,
                configureHttpConnection => configureHttpConnection.AccessTokenProvider = async () => await GetAccessTokenAsync()
                )
            .WithAutomaticReconnect()
            .Build();
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var accessTokenResult = await accessTokenProvider.RequestAccessToken();
        
        if (accessTokenResult.TryGetToken(out var token))
        {
            return token.Value;
        }

        throw new InvalidOperationException("Failed to get an access token.");
    }
}