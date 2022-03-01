using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing hosting started event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnHostingStartedArgs : EventArgs
    {
        /// <summary>
        /// Property representing hosting channel.
        /// </summary>
        public HostingStarted HostingStarted;
    }
}
