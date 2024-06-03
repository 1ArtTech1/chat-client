namespace Chat.Core.Services.Interfaces;

/// <summary>
/// The hub service.
/// </summary>
/// <typeparam name="TSetting">The generic entity type.</typeparam>
public interface IHubService<TSetting>
{
    /// <summary>
    /// Invokes a hub method on the server using the specified method name and entity.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <param name="entity">The entity.</param>
    /// <typeparam name="TEntity">The generic entity type.</typeparam>
    /// <returns>The <see cref="Task"/> a instance.</returns>
    Task InvokeAsync<TEntity>(string name, TEntity entity);

    /// <summary>
    /// Invokes a hub method on the server using the specified method name.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <returns>The <see cref="Task"/> a instance.</returns>
    Task InvokeAsync(string name);

    /// <summary>
    /// Starts a connection to the server.
    /// </summary>
    /// <returns>The <see cref="Task"/> a instance.</returns>
    Task StartAsync();

    /// <summary>
    /// Stops a connection to the server.
    /// </summary>
    /// <returns>The <see cref="Task"/> a instance.</returns>
    Task StopAsync();

    /// <summary>
    /// Registers a handler that will be invoked when the hub method with the specified method name is invoked.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <param name="handler">The entity.</param>
    /// <typeparam name="TEntity">The generic entity type.</typeparam>
    void OnInvoked<TEntity>(string name, Action<TEntity> handler);
}