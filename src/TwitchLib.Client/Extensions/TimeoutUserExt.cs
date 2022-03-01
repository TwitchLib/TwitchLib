using System;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension implementing timeout functionality in TwitchClient
    /// </summary>
    public static class TimeoutUserExt
    {
        #region TimeoutUser
        /// <summary>
        /// TImesout a user in chat using a JoinedChannel object.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">Channel object to send timeout to</param>
        /// <param name="viewer">Viewer name to timeout</param>
        /// <param name="duration">Duration of the timeout via TimeSpan object</param>
        /// <param name="message">Message to accompany the timeout and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not sened if true)</param>
        public static void TimeoutUser(this ITwitchClient client, JoinedChannel channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
        {
            client.SendMessage(channel, $".timeout {viewer} {duration.TotalSeconds} {message}", dryRun);
        }

        /// <summary>
        /// Timesout a user in chat using a string for the channel.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">Channel in string form to send timeout to</param>
        /// <param name="viewer">Viewer name to timeout</param>
        /// <param name="duration">Duration of the timeout via TimeSpan object</param>
        /// <param name="message">Message to accompany the timeout and show the user.</param>
        /// <param name="dryRun">Indicates a dryrun (will not sened if true)</param>
        public static void TimeoutUser(this ITwitchClient client, string channel, string viewer, TimeSpan duration, string message = "", bool dryRun = false)
        {
            JoinedChannel joinedChannel = client.GetJoinedChannel(channel);
            if (joinedChannel != null)
                TimeoutUser(client, joinedChannel, viewer, duration, message, dryRun);
        }
        #endregion
    }
}
