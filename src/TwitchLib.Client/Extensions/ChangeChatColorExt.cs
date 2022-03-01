using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension implementing the change chat color functionality in TwitchClient
    /// </summary>
    public static class ChangeChatColorExt
    {
        /// <summary>
        /// Sends request to change color of chat name in Twitch chat.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel object representing which channel to send command to.</param>
        /// <param name="color">Enum representing available chat preset colors.</param>
        public static void ChangeChatColor(this ITwitchClient client, JoinedChannel channel, Enums.ChatColorPresets color)
        {
            client.SendMessage(channel, $".color {color}");
        }

        /// <summary>
        /// Sends request to change color of chat name in Twitch chat.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representing the channel to send the command to.</param>
        /// <param name="color">Enum representing available chat preset colors.</param>
        public static void ChangeChatColor(this ITwitchClient client, string channel, Enums.ChatColorPresets color)
        {
            client.SendMessage(channel, $".color {color}");
        }
    }
}
