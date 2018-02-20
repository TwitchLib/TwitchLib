using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    /// <summary>Extension for implementing emote only mode functionality in TwitchClient</summary>
    public static class EmoteOnlyExt
    {
        /// <summary>
        /// Enables emote only chat requirement.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of the channel to send the enable emote only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOn(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".emoteonly");
        }

        /// <summary>
        /// Enables emote only chat requirement.
        /// </summary>
        /// <param name="channel">String representation of the channel to send the enable emote only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOn(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".emoteonly");
        }

        /// <summary>
        /// Enables emote only chat requirement.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOn(this TwitchClient client)
        {
            client.SendMessage(".emoteonly");
        }

        /// <summary>
        /// Disables emote only chat requirement.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of the channel to send the disable emote only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOff(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".emoteonlyoff");
        }

        /// <summary>
        /// Disables emote only chat requirement.
        /// </summary>
        /// <param name="channel">String representation of the channel to send the disable emote only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOff(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".emoteonlyoff");
        }

        /// <summary>
        /// Disables emote only chat requirement.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void EmoteOnlyOff(this TwitchClient client)
        {
            client.SendMessage(".emoteonlyoff");
        }
    }
}
