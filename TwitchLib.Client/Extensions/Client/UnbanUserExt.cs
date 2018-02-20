using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Extensions.Client
{
    /// <summary>Extension to implement unban functionality.</summary>
    public static class UnbanUserExt
    {
        /// <summary>
        /// Unbans a user in chat using JoinedChannel
        /// </summary>
        /// <param name="channel">JoinedChannel object to send unban to</param>
        /// <param name="viewer">Viewer name to unban</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void UnbanUser(this TwitchClient client, JoinedChannel channel, string viewer, bool dryRun = false)
        {
            client.SendMessage(channel, $".unban {viewer}", dryRun);
        }

        /// <summary>
        /// Unbans a user in chat using a string for the channel
        /// </summary>
        /// <param name="channel">Channel in string form to send unban to</param>
        /// <param name="viewer">Viewer name to unban</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// /// <param name="client">Client reference used to identify extension.</param>
        public static void UnbanUser(this TwitchClient client, string channel, string viewer, bool dryRun = false)
        {
            var joinedChannel = client.GetJoinedChannel(channel);
            if (joinedChannel != null)
                UnbanUser(client, joinedChannel, viewer, dryRun);
        }

        /// <summary>
        /// Unbans a user in chat using first joined channel.
        /// </summary>
        /// <param name="viewer">Viewer name to unban</param>
        /// <param name="dryRun">Indicates a dryrun (will not send if true)</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void UnbanUser(this TwitchClient client, string viewer, bool dryRun = false)
        {
            if (client.JoinedChannels.Count > 0)
                UnbanUser(client, client.JoinedChannels[0], viewer, dryRun);
        }
    }
}
