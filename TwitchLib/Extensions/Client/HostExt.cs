using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class HostExt
    {
        /// <summary>
        /// Sends command to host a given channel.
        /// </summary>
        /// <param name="userToHost">The channel to be hosted.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the host command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Host(this TwitchClient client, JoinedChannel channel, string userToHost)
        {
            client.SendMessage(channel, $".host {userToHost}");
        }

        /// <summary>
        /// Sends command to host a given channel.
        /// </summary>
        /// <param name="userToHost">The channel to be hosted.</param>
        /// <param name="channel">String representation of which channel to send the host command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Host(this TwitchClient client, string channel, string userToHost)
        {
            client.SendMessage(channel, $".host {userToHost}");
        }

        /// <summary>
        /// Sends command to host a given channel.
        /// </summary>
        /// <param name="userToHost">The channel to be hosted.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Host(this TwitchClient client, string userToHost)
        {
            client.SendMessage($".host {userToHost}");
        }

        /// <summary>
        /// Sends command to unhost if a stream is being hosted.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of the channel to send the unhost command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unhost(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".unhost");
        }

        /// <summary>
        /// Sends command to unhost if a stream is being hosted.
        /// </summary>
        /// <param name="channel">String representation of the channel to send the unhost command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unhost(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".unhost");
        }

        /// <summary>
        /// Sends command to unhost if a stream is being hosted.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unhost(this TwitchClient client)
        {
            client.SendMessage(".unhost");
        }
    }
}
