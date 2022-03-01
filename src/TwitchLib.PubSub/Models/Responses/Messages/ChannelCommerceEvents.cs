using System;
using System.Text.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// Model representing the data in a channel commerce event.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class ChannelCommerceEvents : MessageData
    {
        /// <summary>
        /// Username of the buyer.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; }
        /// <summary>
        /// Display name of the buyer.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; }
        /// <summary>
        /// The channel the purchase was made in.
        /// </summary>
        /// <value>The name of the channel.</value>
        public string ChannelName { get; }
        /// <summary>
        /// User ID of the buyer.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; }
        /// <summary>
        /// Channel/User ID the purchase was made for/in.
        /// </summary>
        /// <value>The channel identifier.</value>
        public string ChannelId { get; }
        /// <summary>
        /// Time stamp of the event.
        /// </summary>
        /// <value>The time.</value>
        public string Time { get; }
        /// <summary>
        /// URL for the item's image.
        /// </summary>
        /// <value>The item image URL.</value>
        public string ItemImageURL { get; }
        /// <summary>
        /// Description of the item.
        /// </summary>
        /// <value>The item description.</value>
        public string ItemDescription { get; }
        /// <summary>
        /// Does this purchase support the channel?
        /// </summary>
        /// <value><c>true</c> if [supports channel]; otherwise, <c>false</c>.</value>
        public bool SupportsChannel { get; }
        /// <summary>
        /// Chat message that accompanied the purchase.
        /// </summary>
        /// <value>The purchase message.</value>
        public string PurchaseMessage { get; }

        /// <summary>
        /// ChannelBitsEvent model constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public ChannelCommerceEvents(string jsonStr)
        {
            var json = JsonDocument.Parse(jsonStr);
            var data = json.RootElement.GetProperty("data");
            Username = data.GetProperty("user_name").GetString();
            DisplayName = data.GetProperty("display_name").GetString();
            ChannelName = data.GetProperty("channel_name").GetString();
            UserId = data.GetProperty("user_id").GetString();
            ChannelId = data.GetProperty("channel_id").GetString();
            Time = data.GetProperty("time").GetString();
            ItemImageURL = data.GetProperty("image_item_url").GetString();
            ItemDescription = data.GetProperty("item_description").GetString();
            SupportsChannel = data.GetProperty("supports_channel").GetBoolean();
            PurchaseMessage = data.GetProperty("purchase_message").GetProperty("message").GetString();
        }
    }
}
