using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Object representing the arguments for channel commerce event
    /// </summary>
    public class OnChannelCommerceReceivedArgs : EventArgs
    {
        /// <summary>
        /// Property for username.
        /// </summary>
        public string Username;
        /// <summary>
        /// Property for buyer's display name.
        /// </summary>
        public string DisplayName;
        /// <summary>
        /// Property for channel's name.
        /// </summary>
        public string ChannelName;
        /// <summary>
        /// Property for buyer's user ID.
        /// </summary>
        public string UserId;
        /// <summary>
        /// Property for channel's ID.
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// Property for timestamp.
        /// </summary>
        public string Time;
        /// <summary>
        /// Property for item's image URL.
        /// </summary>
        public string ItemImageURL;
        /// <summary>
        /// Property for item description.
        /// </summary>
        public string ItemDescription;
        /// <summary>
        /// Property for whether this purchase supports the channel or not.
        /// </summary>
        public bool SupportsChannel;
        /// <summary>
        /// Property for accompanying chat message.
        /// </summary>
        public string PurchaseMessage;
    }
}
