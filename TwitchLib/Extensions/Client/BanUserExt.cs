using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class BanUserExt
    {
        /// <summary>
        /// Bans a user in chat using JoinedChannel
        /// </summary>
        /// <param name="channel">JoinedChannel object to send ban to</param>
        /// <param name="viewer">Viewer name to ban</param>
        /// <param name="message">Message to accompany the ban and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void BanUser(this TwitchClient client, JoinedChannel channel, string viewer, string message = "", bool dryRun = false)
        {
            client.SendMessage(channel, $".ban {viewer} {message}");
        }

        /// <summary>
        /// Bans a user in chat using a string for the channel
        /// </summary>
        /// <param name="channel">Channel in string form to send ban to</param>
        /// <param name="viewer">Viewer name to ban</param>
        /// <param name="message">Message to accompany the ban and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void BanUser(this TwitchClient client, string channel, string viewer, string message = "", bool dryRun = false)
        {
            var joinedChannel = client.GetJoinedChannel(channel);
            if (joinedChannel != null)
                BanUser(client, joinedChannel, viewer, message, dryRun);
        }

        /// <summary>
        /// Bans a user in chat using the first joined channel
        /// </summary>
        /// <param name="viewer">Viewer name to ban</param>
        /// <param name="message">Message to accompany the ban and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void BanUser(this TwitchClient client, string viewer, string message = "", bool dryRun = false)
        {
            if (client.JoinedChannels.Count > 0)
                BanUser(client, client.JoinedChannels[0], viewer, message, dryRun);
        }
    }
}
