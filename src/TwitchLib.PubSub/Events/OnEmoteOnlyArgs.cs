using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of emotes only event.
    /// </summary>
    public class OnEmoteOnlyArgs : EventArgs
    {
        /// <summary>
        /// Property representing moderator who issued moderator only command.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
