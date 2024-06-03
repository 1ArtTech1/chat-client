using Chat.Core.Constants;
using Chat.Core.Models.Settings;

namespace Chat.Ui.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionConfigurationExtensions
{
    /// <summary>
    /// Adds App Settings Injection.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />.</param>
    /// <param name="configuration"><see cref="IConfiguration" />.</param>
    public static void AddAppSettingsOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ChatHubSetting>(options =>
        {
            configuration.GetSection(ChatApiConfigurations.ChatHub).Bind(options);
        });
        
        services.Configure<MessageHubSetting>(options =>
        {
            configuration.GetSection(ChatApiConfigurations.MessageHub).Bind(options);
        });
    }
}