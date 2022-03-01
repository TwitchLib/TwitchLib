using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension for implementing Commercial functionality in TwitchClient.
    /// </summary>
    public static class GetChannelModeratorsExt
    {
        /// <summary>
        /// Sends command to get list of moderators from Twitch.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of the channel to send the command to get moderators to.</param>
        public static void GetChannelModerators(this ITwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".mods");
        }

        /// <summary>
        /// Sends command to get list of moderators from Twitch.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of the channel to send the command to get moderators to.</param>
        public static void GetChannelModerators(this ITwitchClient client, string channel)
        {
            client.SendMessage(channel, ".mods");
        }
    }
}
