using System;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    /// <summary></summary>
    public class OnWhisperReceivedArgs : EventArgs
    {
        /// <summary></summary>
        public WhisperMessage WhisperMessage;
    }
}
