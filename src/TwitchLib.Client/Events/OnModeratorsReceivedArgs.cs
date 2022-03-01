using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a list of moderators received from chat.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnModeratorsReceivedArgs : EventArgs
    {
        /// <summary>
        /// Property representing the channel the moderator list came from.
        /// </summary>
        public string Channel;
        /// <summary>
        /// Property representing the list of moderators.
        /// </summary>
        public List<string> Moderators;
    }
}
