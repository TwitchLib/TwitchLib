using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension implementing the ability to start raids via TwitchClient.
    /// </summary>
    public static class RaidExt
    {
        /// <summary>
        /// Sends command to start raid.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="channelToRaid">Channel to begin raid on.</param>
        public static void Raid(this ITwitchClient client, JoinedChannel channel, string channelToRaid)
        {
            client.SendMessage(channel, $".raid {channelToRaid}");
        }

        /// <summary>
        /// Sends command to start raid.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to send the command to.</param>
        /// <param name="channelToRaid">Channel to begin raid on.</param>
        public static void Raid(this ITwitchClient client, string channel, string channelToRaid)
        {
            client.SendMessage(channel, $".raid {channelToRaid}");
        }
    }
}
