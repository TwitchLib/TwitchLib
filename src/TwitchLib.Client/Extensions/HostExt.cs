using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension for implementing host functionality in TwitchClient.
    /// </summary>
    public static class HostExt
    {
        /// <summary>
        /// Sends command to host a given channel.r
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the host command to.</param>
        /// <param name="userToHost">The channel to be hosted.</param>
        public static void Host(this ITwitchClient client, JoinedChannel channel, string userToHost)
        {
            client.SendMessage(channel, $".host {userToHost}");
        }

        /// <summary>
        /// Sends command to host a given channel.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to send the host command to.</param>
        /// <param name="userToHost">The channel to be hosted.</param>
        public static void Host(this ITwitchClient client, string channel, string userToHost)
        {
            client.SendMessage(channel, $".host {userToHost}");
        }

        /// <summary>
        /// Sends command to unhost if a stream is being hosted.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of the channel to send the unhost command to.</param>
        public static void Unhost(this ITwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".unhost");
        }

        /// <summary>
        /// Sends command to unhost if a stream is being hosted.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of the channel to send the unhost command to.</param>
        public static void Unhost(this ITwitchClient client, string channel)
        {
            client.SendMessage(channel, ".unhost");
        }
    }
}
