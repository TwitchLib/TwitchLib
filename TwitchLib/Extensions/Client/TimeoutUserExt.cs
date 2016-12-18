using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class TimeoutUserExt
    {
        #region TimeoutUser
        /// <summary>
        /// TImesout a user in chat using a JoinedChannel object.
        /// </summary>
        /// <param name="channel">Channel object to send timeout to</param>
        /// <param name="viewer">Viewer name to timeout</param>
        /// <param name="duration">Duration of the timeout via TimeSpan object</param>
        /// <param name="message">Message to accompany the timeout and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not sened if true)</param>
        public static void TimeoutUser(this TwitchClient client, JoinedChannel channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
        {
            client.SendMessage(channel, $".timeout {viewer} {duration.TotalSeconds} {message}", dryRun);
        }

        /// <summary>
        /// Timesout a user in chat using a string for the channel.
        /// </summary>
        /// <param name="channel">Channel in string form to send timeout to</param>
        /// <param name="viewer">Viewer name to timeout</param>
        /// <param name="duration">Duration of the timeout via TimeSpan object</param>
        /// <param name="message">Message to accompany the timeout and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not sened if true)</param>
        public static void TimeoutUser(this TwitchClient client, string channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
        {
            var joinedChannel = client.GetJoinedChannel(channel);
            if (joinedChannel != null)
                TimeoutUser(client, joinedChannel, viewer, duration, message, dryRun);
        }

        /// <summary>
        /// Timesout a user using the first joined channel.
        /// </summary>
        /// <param name="viewer">Viewer name to timeout</param>
        /// <param name="duration">Duration of the timeout via TimeSpan object</param>
        /// <param name="message">Message to accompany the timeout and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not sened if true)</param>
        public static void TimeoutUser(this TwitchClient client, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
        {
            if (client.JoinedChannels.Count > 0)
                TimeoutUser(client, client.JoinedChannels[0], viewer, duration, message, dryRun);
        }
        #endregion
    }
}
