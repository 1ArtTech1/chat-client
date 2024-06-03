namespace Chat.Core.Models.ViewModels;

/// <summary>
/// Create a message view model.
/// </summary>
public class CreateMessageViewModel
{
    /// <summary>
    /// Gets or sets a chat ID.
    /// </summary>
    public int ChatId { get; set; }
    
    /// <summary>
    /// Gets or sets a value.
    /// </summary>
    public string Value { get; set; }
    
    /// <summary>
    /// Gets or sets a date time.
    /// </summary>
    public DateTime DateTime { get; set; }
}