using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Services.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing EventArgs for the OnChannelsSet event.
    /// </summary>
    public class OnChannelsSetArgs : EventArgs
    {
        /// <summary>
        /// The channels the service is currently monitoring.
        /// </summary>
        public List<string> Channels;
    }
}