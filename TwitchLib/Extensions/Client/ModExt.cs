using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class ModExt
    {
        /// <summary>
        /// Sends a command to make a specific viewer a moderator.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="viewerToMod">Username of the viewer to make a moderator.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Mod(this TwitchClient client, JoinedChannel channel, string viewerToMod)
        {
            client.SendMessage(channel, $".mod {viewerToMod}");
        }

        /// <summary>
        /// Sends a command to make a specific viewer a moderator.
        /// </summary>
        /// <param name="channel">String representation of which channel to send the command to.</param>
        /// <param name="viewerToMod">Username of the viewer to make a moderator.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Mod(this TwitchClient client, string channel, string viewerToMod)
        {
            client.SendMessage(channel, $".mod {viewerToMod}");
        }

        /// <summary>
        /// Sends a command to make a specific viewer a moderator.
        /// </summary>
        /// <param name="viewerToMod">Username of the viewer to make a moderator.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Mod(this TwitchClient client, string viewerToMod)
        {
            client.SendMessage($".mod {viewerToMod}");
        }

        /// <summary>
        /// Sends a command to remove moderator status from a specific viewer
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="viewerToUnmod">Username of the viewer to remove moderator status from.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unmod(this TwitchClient client, JoinedChannel channel, string viewerToUnmod)
        {
            client.SendMessage(channel, $".unmod {viewerToUnmod}");
        }

        /// <summary>
        /// Sends a command to remove moderator status from a specific viewer
        /// </summary>
        /// <param name="channel">String representation of which channel to send the command to.</param>
        /// <param name="viewerToUnmod">Username of the viewer to remove moderator status from.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unmod(this TwitchClient client, string channel, string viewerToUnmod)
        {
            client.SendMessage(channel, $".unmod {viewerToUnmod}");
        }

        /// <summary>
        /// Sends a command to remove moderator status from a specific viewer
        /// </summary>
        /// <param name="viewerToUnmod">Username of the viewer to remove moderator status from.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void Unmod(this TwitchClient client, string viewerToUnmod)
        {
            client.SendMessage($".unmod {viewerToUnmod}");
        }
    }
}
