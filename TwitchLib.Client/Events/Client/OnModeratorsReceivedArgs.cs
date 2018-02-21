using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing a list of moderators received from chat.</summary>
    public class OnModeratorsReceivedArgs : EventArgs
    {
        /// <summary>Property representing the channel the moderator list came from.</summary>
        public string Channel;
        /// <summary>Property representing the list of moderators.</summary>
        public List<string> Moderators;
    }
}
