using System;
using System.Collections.Generic;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing whisper command received event.</summary>
    public class OnWhisperCommandReceivedArgs : EventArgs
    {
        /// <summary>Property representing chat message object.</summary>
        public WhisperMessage WhisperMessage;
        /// <summary>Property representing received command.</summary>
        public string Command;
        /// <summary>Property representing arguements in form of string.</summary>
        public string ArgumentsAsString;
        /// <summary>Property representing arguements in form of string list.</summary>
        public List<string> ArgumentsAsList;
        /// <summary>Property representing the character command identifier.</summary>
        public char CommandIdentifier;
    }
}
