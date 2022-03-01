using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a user was timed out event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnUserTimedoutArgs : EventArgs
    {
        /// <summary>
        /// The user timeout
        /// </summary>
        public UserTimeout UserTimeout;
    }
}
