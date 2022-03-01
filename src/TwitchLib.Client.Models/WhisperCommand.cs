using System.Collections.Generic;
using System.Linq;

namespace TwitchLib.Client.Models
{
    /// <summary>Object representing a command received via Twitch chat.</summary>
    public class WhisperCommand
    {
        /// <summary>Property representing all arguments received in a List form.</summary>
        public List<string> ArgumentsAsList { get; }

        /// <summary>Property representing all arguments received in a string form.</summary>
        public string ArgumentsAsString { get; }

        /// <summary>Property representing the command identifier (ie command prefix).</summary>
        public char CommandIdentifier { get; }

        /// <summary>Property representing the actual command (without the command prefix).</summary>
        public string CommandText { get; }

        /// <summary>Property representing the chat message that the command came in.</summary>
        public WhisperMessage WhisperMessage { get; }

        /// <summary>ChatCommand constructor.</summary>
        /// <param name="whisperMessage"></param>
        public WhisperCommand(WhisperMessage whisperMessage)
        {
            WhisperMessage = whisperMessage;
            CommandText = whisperMessage.Message.Split(' ')?[0].Substring(1, whisperMessage.Message.Split(' ')[0].Length - 1) ?? whisperMessage.Message.Substring(1, whisperMessage.Message.Length - 1);
            ArgumentsAsString = whisperMessage.Message.Replace(whisperMessage.Message.Split(' ')?[0] + " ", "");
            ArgumentsAsList = whisperMessage.Message.Split(' ')?.Where(arg => arg != whisperMessage.Message[0] + CommandText).ToList() ?? new List<string>();
            CommandIdentifier = whisperMessage.Message[0];
        }

        public WhisperCommand(
            WhisperMessage whisperMessage,
            string commandText,
            string argumentsAsString,
            List<string> argumentsAsList,
            char commandIdentifier)
        {
            WhisperMessage = whisperMessage;
            CommandText = commandText;
            ArgumentsAsString = argumentsAsString;
            ArgumentsAsList = argumentsAsList;
            CommandIdentifier = commandIdentifier;
        }
    }
}
