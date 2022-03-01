using System;
using System.Collections.Generic;
using TwitchLib.PubSub.Models;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of on leaderboard event.
    /// </summary>
    public class OnLeaderboardEventArgs : EventArgs
    {
        /// <summary>
        /// Property representing channel id where the event got issued.
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// Property representing a leaderboard list.
        /// </summary>
        public List<LeaderBoard> TopList;
    }
}
