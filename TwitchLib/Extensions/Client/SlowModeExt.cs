using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class SlowModeExt
    {
        /// <summary>
        /// Enables slow mode. messageCooldown must be less than 1 day.
        /// </summary>
        /// <param name="messageCooldown">TimeSpan object representing how long message cooldowns should be. May not exceed 1 day total.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send the slow command to.</param>
        public static void SlowModeOn(this TwitchClient client, JoinedChannel channel, TimeSpan messageCooldown)
        {
            if (messageCooldown > TimeSpan.FromDays(1))
                throw new Exceptions.Client.InvalidParameterException("The message cooldown time supplied exceeded the maximum allowed by Twitch, which is 1 day.", client.TwitchUsername);

            client.SendMessage(channel, $".slow {messageCooldown.TotalSeconds}");
        }

        /// <summary>
        /// Enables slow mode. messageCooldown must be less than 1 day.
        /// </summary>
        /// <param name="messageCooldown">TimeSpan object representing how long message cooldowns should be. May not exceed 1 day total.</param>
        /// <param name="channel">String representation of which channel to send the slow command to.</param>
        public static void SlowModeOn(this TwitchClient client, string channel, TimeSpan messageCooldown)
        {
            if (messageCooldown > TimeSpan.FromDays(1))
                throw new Exceptions.Client.InvalidParameterException("The message cooldown time supplied exceeded the maximum allowed by Twitch, which is 1 day.", client.TwitchUsername);

            client.SendMessage(channel, $".slow {messageCooldown.TotalSeconds}");
        }

        /// <summary>
        /// Enables slow mode. messageCooldown must be less than 1 day.
        /// </summary>
        /// <param name="messageCooldown">TimeSpan object representing how long message cooldowns should be. May not exceed 1 day total.</param>
        public static void SlowModeOn(this TwitchClient client, TimeSpan messageCooldown)
        {
            if (messageCooldown > TimeSpan.FromDays(1))
                throw new Exceptions.Client.InvalidParameterException("The message cooldown time supplied exceeded the maximum allowed by Twitch, which is 1 day.", client.TwitchUsername);

            client.SendMessage($".slow {messageCooldown.TotalSeconds}");
        }

        /// <summary>
        /// Disables slow mode.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send slowoff command to.</param>
        public static void SlowModeoff(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".slowoff");
        }

        /// <summary>
        /// Disables slow mode.
        /// </summary>
        /// <param name="channel">String representation of which channel to send slowoff command to.</param>
        public static void SlowModeOff(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".slowoff");
        }

        /// <summary>
        /// Disables slow mode.
        /// </summary>
        public static void SlowModeOff(this TwitchClient client)
        {
            client.SendMessage(".slowoff");
        }
    }
}
