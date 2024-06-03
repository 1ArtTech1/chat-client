namespace Chat.Core.Constants;

/// <summary>
/// Contains a hub constants.
/// </summary>
public static class HubConstants
{
    /// <summary>
    /// Contains a route constants.
    /// </summary>
    public static class Route
    {
        /// <summary>
        /// The roure for the create entity.
        /// </summary>
        public const string CreateAsync = nameof(CreateAsync);

        /// <summary>
        /// The roure for the gets all entity.
        /// </summary>
        public const string GetAllAsync = nameof(GetAllAsync);

        /// <summary>
        /// The roure for the get by chat ID entity.
        /// </summary>
        public const string GetByChatIdAsync = nameof(GetByChatIdAsync);

        /// <summary>
        /// The roure for the join group.
        /// </summary>
        public const string JoinGroup = nameof(JoinGroup);
    }
}