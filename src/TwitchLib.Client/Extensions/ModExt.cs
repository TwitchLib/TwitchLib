using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension implementing the ability to mod a viewer in TwitchClient.
    /// </summary>
    public static class ModExt
    {
        /// <summary>
        /// Sends a command to make a specific viewer a moderator.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="viewerToMod">Username of the viewer to make a moderator.</param>
        public static void Mod(this ITwitchClient client, JoinedChannel channel, string viewerToMod)
        {
            client.SendMessage(channel, $".mod {viewerToMod}");
        }

        /// <summary>
        /// Sends a command to make a specific viewer a moderator.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to send the command to.</param>
        /// <param name="viewerToMod">Username of the viewer to make a moderator.</param>
        public static void Mod(this ITwitchClient client, string channel, string viewerToMod)
        {
            client.SendMessage(channel, $".mod {viewerToMod}");
        }

        /// <summary>
        /// Sends a command to remove moderator status from a specific viewer
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the command to.</param>
        /// <param name="viewerToUnmod">Username of the viewer to remove moderator status from.</param>
        public static void Unmod(this ITwitchClient client, JoinedChannel channel, string viewerToUnmod)
        {
            client.SendMessage(channel, $".unmod {viewerToUnmod}");
        }

        /// <summary>
        /// Sends a command to remove moderator status from a specific viewer
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to send the command to.</param>
        /// <param name="viewerToUnmod">Username of the viewer to remove moderator status from.</param>
        public static void Unmod(this ITwitchClient client, string channel, string viewerToUnmod)
        {
            client.SendMessage(channel, $".unmod {viewerToUnmod}");
        }
    }
}
