using System;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing client connection error event.</summary>
    public class OnConnectionErrorArgs : EventArgs
    {
        /// <summary></summary>
        public ErrorEvent Error;
        /// <summary>Username of the bot that suffered connection error.</summary>
        public string BotUsername;
    }
}
