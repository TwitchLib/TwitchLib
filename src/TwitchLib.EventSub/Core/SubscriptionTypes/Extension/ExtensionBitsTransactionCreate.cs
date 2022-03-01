using TwitchLib.EventSub.Core.Models.Extensions;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Extension
{
    /// <summary>
    /// Extension Bits Transaction Create subscription type model
    /// <para>Description:</para>
    /// <para>A Bits transaction occurred for a specified Twitch Extension.</para>
    /// </summary>
    public class ExtensionBitsTransactionCreate
    {
        /// <summary>
        /// Transaction ID.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Client ID of the extension.
        /// </summary>
        public string ExtensionClientId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's broadcaster ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's user ID.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's user login.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The transaction's user display name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Additional extension product information.
        /// </summary>
        public BitsProduct Product { get; set; } = new();
    }
}