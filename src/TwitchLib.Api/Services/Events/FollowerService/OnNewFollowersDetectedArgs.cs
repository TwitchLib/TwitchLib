using System;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing EventArgs for OnNewFollowersDetected event.
    /// </summary>
    public class OnNewFollowersDetectedArgs : EventArgs
    {
        /// <summary>Event property representing channel the service is currently monitoring.</summary>
        public string Channel;
        /// <summary>Event property representing all new followers detected.</summary>
        public List<Follow> NewFollowers;
    }
}
