using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension for implementing clear chat functionality in TwitchClient.
    /// </summary>
    public static class ClearChatExt
    {
        /// <summary>
        /// Sends request to clear chat (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send clear chat command to.</param>
        public static void ClearChat(this ITwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".clear");
        }

        /// <summary>
        /// Sends request to clear chat (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to send clear chat command to.</param>
        public static void ClearChat(this ITwitchClient client, string channel)
        {
            client.SendMessage(channel, ".clear");
        }
    }
}
