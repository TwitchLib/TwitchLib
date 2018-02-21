using System;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing on user state changed event.</summary>
    public class OnUserStateChangedArgs : EventArgs
    {
        /// <summary>Property representing user state object.</summary>
        public UserState UserState;
    }
}
