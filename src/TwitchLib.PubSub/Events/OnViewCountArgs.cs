using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// ViewCount arguments class.
    /// </summary>
    public class OnViewCountArgs : EventArgs
    {
        /// <summary>
        /// Server time issued by Twitch.
        /// </summary>
        public string ServerTime;
        /// <summary>
        /// Number of viewers at current time.
        /// </summary>
        public int Viewers;
        /// <summary>
        /// Property representing the id of the channel the event originated from.
        /// </summary>
        public string ChannelId;
    }
}
