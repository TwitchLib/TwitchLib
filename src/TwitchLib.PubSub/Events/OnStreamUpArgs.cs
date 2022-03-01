using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing when a stream starts event.
    /// </summary>
    public class OnStreamUpArgs : EventArgs
    {
        /// <summary>
        /// Property representing the server time.
        /// </summary>
        public string ServerTime;
        /// <summary>
        /// Property representing play delay.
        /// </summary>
        public int PlayDelay;
        /// <summary>
        /// Property representing the id of the channel the event originated from.
        /// </summary>
        public string ChannelId;
    }
}
