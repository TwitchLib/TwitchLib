using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class OnLogArgs.
    /// </summary>
    public class OnLogArgs : EventArgs
    {
        /// <summary>
        /// Property representing data received from Twitch
        /// </summary>
        public string Data;
    }
}
