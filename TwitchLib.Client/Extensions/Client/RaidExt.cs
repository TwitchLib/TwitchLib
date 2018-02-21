using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Extensions.Client
{
    /// <summary>Extension implementing the ability to start raids via TwitchClient.</summary>
    public static class RaidExt
    {
        /// <summary>
        /// Sends command to start raid.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="channelToRaid">Channel to begin raid on.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Raid(this TwitchClient client, JoinedChannel channel, string channelToRaid)
        {
            client.SendMessage(channel, $".raid {channelToRaid}");
        }
    }
}
